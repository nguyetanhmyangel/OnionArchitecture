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
    public class LabelMyBaseRepository : ILabelMyBaseRepository
    {
        private readonly IRepositoryAsync<LabelMyBase> _repository;
        private readonly IDistributedCache _distributedCache;

        public LabelMyBaseRepository(IDistributedCache distributedCache, IRepositoryAsync<LabelMyBase> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<LabelMyBase> LabelMyBases => _repository.Entities;

        public async Task DeleteAsync(LabelMyBase labelMyBase)
        {
            await _repository.DeleteAsync(labelMyBase);
            await _distributedCache.RemoveAsync(LabelMyBaseCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(LabelMyBaseCacheKeys.GetKey(labelMyBase.Id));
        }

        public async Task<LabelMyBase> GetByIdAsync(int labelMyBaseId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = LabelMyBaseCacheKeys.GetKey(labelMyBaseId);
            var labelMyBase = await _distributedCache.GetAsync<LabelMyBase>(cacheKey);
            if (labelMyBase == null)
            {
                labelMyBase = await _repository.Entities.Where(p => p.Id == labelMyBaseId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(labelMyBase, "Comment", "No Comment Found");
                await _distributedCache.SetAsync(cacheKey, labelMyBase);
            }
            return labelMyBase;
        }

        public async Task<List<LabelMyBase>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = LabelMyBaseCacheKeys.ListKey;
            var labelMyBaseList = await _distributedCache.GetAsync<List<LabelMyBase>>(cacheKey);
            if (labelMyBaseList == null)
            {
                labelMyBaseList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, labelMyBaseList);
            }
            return labelMyBaseList;
        }

        public async Task<int> InsertAsync(LabelMyBase labelMyBase)
        {
            await _repository.AddAsync(labelMyBase);
            await _distributedCache.RemoveAsync(LabelMyBaseCacheKeys.ListKey);
            return labelMyBase.Id;
        }

        public async Task UpdateAsync(LabelMyBase labelMyBase)
        {
            await _repository.UpdateAsync(labelMyBase);
            await _distributedCache.RemoveAsync(LabelMyBaseCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(LabelMyBaseCacheKeys.GetKey(labelMyBase.Id));
        }
    }
}