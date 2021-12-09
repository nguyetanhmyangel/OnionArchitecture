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
    public class MySpaceRepository : IMySpaceRepository
    {
        private readonly IRepositoryAsync<MySpace> _repository;
        private readonly IDistributedCache _distributedCache;

        public MySpaceRepository(IDistributedCache distributedCache, IRepositoryAsync<MySpace> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<MySpace> MySpaces => _repository.Entities;

        public async Task DeleteAsync(MySpace labelMyBase)
        {
            await _repository.DeleteAsync(labelMyBase);
            await _distributedCache.RemoveAsync(MySpaceCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(MySpaceCacheKeys.GetKey(labelMyBase.Id));
        }

        public async Task<MySpace> GetByIdAsync(int myBaseId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = MySpaceCacheKeys.GetKey(myBaseId);
            var myBase = await _distributedCache.GetAsync<MySpace>(cacheKey);
            if (myBase == null)
            {
                myBase = await _repository.Entities.Where(p => p.Id == myBaseId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(myBase, "MySpace", "No MySpace Found");
                await _distributedCache.SetAsync(cacheKey, myBase);
            }
            return myBase;
        }

        public async Task<List<MySpace>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = MySpaceCacheKeys.ListKey;
            var myBaseList = await _distributedCache.GetAsync<List<MySpace>>(cacheKey);
            if (myBaseList == null)
            {
                myBaseList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, myBaseList);
            }
            return myBaseList;
        }

        public async Task<int> InsertAsync(MySpace myBase)
        {
            await _repository.AddAsync(myBase);
            await _distributedCache.RemoveAsync(MySpaceCacheKeys.ListKey);
            return myBase.Id;
        }

        public async Task UpdateAsync(MySpace myBase)
        {
            await _repository.UpdateAsync(myBase);
            await _distributedCache.RemoveAsync(MySpaceCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(MySpaceCacheKeys.GetKey(myBase.Id));
        }
    }
}