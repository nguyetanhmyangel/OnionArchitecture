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
    public class LabelMySpaceRepository : ILabelMySpaceRepository
    {
        private readonly IRepositoryAsync<LabelMySpace> _repository;
        private readonly IDistributedCache _distributedCache;

        public LabelMySpaceRepository(IDistributedCache distributedCache, IRepositoryAsync<LabelMySpace> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<LabelMySpace> LabelMySpaces => _repository.Entities;

        public async Task DeleteAsync(LabelMySpace labelMyBase)
        {
            await _repository.DeleteAsync(labelMyBase);
            await _distributedCache.RemoveAsync(LabelMySpaceCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(LabelMySpaceCacheKeys.GetKey(labelMyBase.Id));
        }

        public async Task<LabelMySpace> GetByIdAsync(int labelMyBaseId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = LabelMySpaceCacheKeys.GetKey(labelMyBaseId);
            var labelMyBase = await _distributedCache.GetAsync<LabelMySpace>(cacheKey);
            if (labelMyBase == null)
            {
                labelMyBase = await _repository.Entities.Where(p => p.Id == labelMyBaseId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(labelMyBase, "Comment", "No Comment Found");
                await _distributedCache.SetAsync(cacheKey, labelMyBase);
            }
            return labelMyBase;
        }

        public async Task<List<LabelMySpace>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = LabelMySpaceCacheKeys.ListKey;
            var labelMyBaseList = await _distributedCache.GetAsync<List<LabelMySpace>>(cacheKey);
            if (labelMyBaseList == null)
            {
                labelMyBaseList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, labelMyBaseList);
            }
            return labelMyBaseList;
        }

        public async Task<int> InsertAsync(LabelMySpace labelMyBase)
        {
            await _repository.AddAsync(labelMyBase);
            await _distributedCache.RemoveAsync(LabelMySpaceCacheKeys.ListKey);
            return labelMyBase.Id;
        }

        public async Task UpdateAsync(LabelMySpace labelMyBase)
        {
            await _repository.UpdateAsync(labelMyBase);
            await _distributedCache.RemoveAsync(LabelMySpaceCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(LabelMySpaceCacheKeys.GetKey(labelMyBase.Id));
        }
    }
}