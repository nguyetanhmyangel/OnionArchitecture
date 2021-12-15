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
    public class LabelKnowledgeBaseRepository : ILabelKnowledgeBaseRepository
    {
        private readonly IRepositoryAsync<LabelKnowledgeBase> _repository;
        private readonly IDistributedCache _distributedCache;

        public LabelKnowledgeBaseRepository(IDistributedCache distributedCache, IRepositoryAsync<LabelKnowledgeBase> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<LabelKnowledgeBase> LabelKnowledgeBases => _repository.Entities;

        public async Task DeleteAsync(LabelKnowledgeBase labelKnowledgeBase)
        {
            await _repository.DeleteAsync(labelKnowledgeBase);
            await _distributedCache.RemoveAsync(LabelKnowledgeBaseCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(LabelKnowledgeBaseCacheKeys.GetKey(labelKnowledgeBase.Id));
        }

        public async Task<LabelKnowledgeBase> GetByIdAsync(int labelKnowledgeBaseId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = LabelKnowledgeBaseCacheKeys.GetKey(labelKnowledgeBaseId);
            var labelKnowledgeBase = await _distributedCache.GetAsync<LabelKnowledgeBase>(cacheKey);
            if (labelKnowledgeBase == null)
            {
                labelKnowledgeBase = await _repository.Entities.Where(p => p.Id == labelKnowledgeBaseId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(labelKnowledgeBase, "LabelMySpace", "No LabelMySpace Found");
                await _distributedCache.SetAsync(cacheKey, labelKnowledgeBase);
            }
            return labelKnowledgeBase;
        }

        public async Task<List<LabelKnowledgeBase>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = LabelKnowledgeBaseCacheKeys.ListKey;
            var labelKnowledgeBaseList = await _distributedCache.GetAsync<List<LabelKnowledgeBase>>(cacheKey);
            if (labelKnowledgeBaseList == null)
            {
                labelKnowledgeBaseList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, labelKnowledgeBaseList);
            }
            return labelKnowledgeBaseList;
        }

        public async Task<int> InsertAsync(LabelKnowledgeBase labelKnowledgeBase)
        {
            await _repository.AddAsync(labelKnowledgeBase);
            await _distributedCache.RemoveAsync(LabelKnowledgeBaseCacheKeys.ListKey);
            return labelKnowledgeBase.Id;
        }

        public async Task UpdateAsync(LabelKnowledgeBase labelKnowledgeBase)
        {
            await _repository.UpdateAsync(labelKnowledgeBase);
            await _distributedCache.RemoveAsync(LabelKnowledgeBaseCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(LabelKnowledgeBaseCacheKeys.GetKey(labelKnowledgeBase.Id));
        }
    }
}