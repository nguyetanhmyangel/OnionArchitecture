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
    public class LabelRepository : ILabelRepository
    {
        private readonly IRepositoryAsync<Label> _repository;
        private readonly IDistributedCache _distributedCache;

        public LabelRepository(IDistributedCache distributedCache, IRepositoryAsync<Label> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Label> Labels => _repository.Entities;

        public async Task DeleteAsync(Label label)
        {
            await _repository.DeleteAsync(label);
            await _distributedCache.RemoveAsync(LabelCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(LabelCacheKeys.GetKey(label.Id));
        }

        public async Task<Label> GetByIdAsync(int labelId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = LabelCacheKeys.GetKey(labelId);
            var label = await _distributedCache.GetAsync<Label>(cacheKey);
            if (label == null)
            {
                label = await _repository.Entities.Where(p => p.Id == labelId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(label, "Comment", "No Comment Found");
                await _distributedCache.SetAsync(cacheKey, label);
            }
            return label;
        }

        public async Task<List<Label>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = LabelCacheKeys.ListKey;
            var labelList = await _distributedCache.GetAsync<List<Label>>(cacheKey);
            if (labelList == null)
            {
                labelList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, labelList);
            }
            return labelList;
        }

        public async Task<int> InsertAsync(Label label)
        {
            await _repository.AddAsync(label);
            await _distributedCache.RemoveAsync(LabelCacheKeys.ListKey);
            return label.Id;
        }

        public async Task UpdateAsync(Label label)
        {
            await _repository.UpdateAsync(label);
            await _distributedCache.RemoveAsync(LabelCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(LabelCacheKeys.GetKey(label.Id));
        }
    }
}