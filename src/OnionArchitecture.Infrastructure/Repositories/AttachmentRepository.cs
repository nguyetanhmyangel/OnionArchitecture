using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Infrastructure.CacheKeys;
using OnionArchitecture.Infrastructure.Share.Caching;
using OnionArchitecture.Infrastructure.Share.ThrowR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnionArchitecture.Infrastructure.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly IRepositoryAsync<Attachment> _repository;
        private readonly IDistributedCache _distributedCache;

        public AttachmentRepository(IDistributedCache distributedCache, IRepositoryAsync<Attachment> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Attachment> Attachments => _repository.Entities;

        public async Task DeleteAsync(Attachment attachment)
        {
            await _repository.DeleteAsync(attachment);
            await _distributedCache.RemoveAsync(AttachmentCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(AttachmentCacheKeys.GetKey(attachment.Id));
        }

        public async Task<Attachment> GetByIdAsync(int attachmentId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == attachmentId).FirstOrDefaultAsync();

            var cacheKey = AttachmentCacheKeys.GetKey(attachmentId);
            var attachment = await _distributedCache.GetAsync<Attachment>(cacheKey);
            if (attachment == null)
            {
                attachment = await _repository.Entities.Where(p => p.Id == attachmentId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(attachment, "Attachment", "No Attachment Found");
                await _distributedCache.SetAsync(cacheKey, attachment);
            }
            return attachment;
        }

        public async Task<List<Attachment>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = AttachmentCacheKeys.ListKey;
            var attachmentList = await _distributedCache.GetAsync<List<Attachment>>(cacheKey);
            if (attachmentList == null)
            {
                attachmentList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, attachmentList);
            }
            return attachmentList;
        }

        public async Task<int> InsertAsync(Attachment attachment)
        {
            await _repository.AddAsync(attachment);
            await _distributedCache.RemoveAsync(AttachmentCacheKeys.ListKey);
            return attachment.Id;
        }

        public async Task UpdateAsync(Attachment attachment)
        {
            await _repository.UpdateAsync(attachment);
            await _distributedCache.RemoveAsync(AttachmentCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(AttachmentCacheKeys.GetKey(attachment.Id));
        }
    }
}