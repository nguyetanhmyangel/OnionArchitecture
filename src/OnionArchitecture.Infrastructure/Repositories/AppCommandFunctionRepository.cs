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
    public class AppCommandFunctionRepository : IAppCommandFunctionRepository
    {
        private readonly IRepositoryAsync<AppCommandFunction> _repository;
        private readonly IDistributedCache _distributedCache;

        public AppCommandFunctionRepository(IDistributedCache distributedCache, IRepositoryAsync<AppCommandFunction> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<AppCommandFunction> AppCommandFunctions => _repository.Entities;

        public async Task DeleteAsync(AppCommandFunction appCommandFunction)
        {
            await _repository.DeleteAsync(appCommandFunction);
            await _distributedCache.RemoveAsync(AppCommandFunctionCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(AppCommandFunctionCacheKeys.GetKey(appCommandFunction.Id));
        }

        public async Task<AppCommandFunction> GetByIdAsync(int appCommandFunctionId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = AppCommandFunctionCacheKeys.GetKey(appCommandFunctionId);
            var appCommandFunction = await _distributedCache.GetAsync<AppCommandFunction>(cacheKey);
            if (appCommandFunction == null)
            {
                appCommandFunction = await _repository.Entities.Where(p => p.Id == appCommandFunctionId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(appCommandFunction, "appCommandFunction", "No appCommandFunction Found");
                await _distributedCache.SetAsync(cacheKey, appCommandFunction);
            }
            return appCommandFunction;
        }

        public async Task<List<AppCommandFunction>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = AppCommandFunctionCacheKeys.ListKey;
            var appCommandFunctionList = await _distributedCache.GetAsync<List<AppCommandFunction>>(cacheKey);
            if (appCommandFunctionList == null)
            {
                appCommandFunctionList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, appCommandFunctionList);
            }
            return appCommandFunctionList;
        }

        public async Task<int> InsertAsync(AppCommandFunction appCommandFunction)
        {
            await _repository.AddAsync(appCommandFunction);
            await _distributedCache.RemoveAsync(AppCommandFunctionCacheKeys.ListKey);
            return appCommandFunction.Id;
        }

        public async Task UpdateAsync(AppCommandFunction appCommandFunction)
        {
            await _repository.UpdateAsync(appCommandFunction);
            await _distributedCache.RemoveAsync(AppCommandFunctionCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(AppCommandFunctionCacheKeys.GetKey(appCommandFunction.Id));
        }
    }
}