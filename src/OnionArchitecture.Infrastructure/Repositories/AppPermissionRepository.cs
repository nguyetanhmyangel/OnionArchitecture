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
    public class AppPermissionRepository : IAppPermissionRepository
    {
        private readonly IRepositoryAsync<AppPermission> _repository;
        private readonly IDistributedCache _distributedCache;

        public AppPermissionRepository(IDistributedCache distributedCache, IRepositoryAsync<AppPermission> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<AppPermission> AppPermission => _repository.Entities;


        public async Task DeleteAsync(AppPermission appPermission)
        {
            await _repository.DeleteAsync(appPermission);
            await _distributedCache.RemoveAsync(AppPermissionCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(AppPermissionCacheKeys.GetKey(appPermission.Id));
        }

        public async Task<AppPermission> GetByIdAsync(int appPermissionId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = AppPermissionCacheKeys.GetKey(appPermissionId);
            var labelMyBase = await _distributedCache.GetAsync<AppPermission>(cacheKey);
            if (labelMyBase == null)
            {
                labelMyBase = await _repository.Entities.Where(p => p.Id == appPermissionId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(labelMyBase, "appPermission", "No appPermission Found");
                await _distributedCache.SetAsync(cacheKey, labelMyBase);
            }
            return labelMyBase;
        }

        public async Task<List<AppPermission>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = AppPermissionCacheKeys.ListKey;
            var appPermissionList = await _distributedCache.GetAsync<List<AppPermission>>(cacheKey);
            if (appPermissionList == null)
            {
                appPermissionList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, appPermissionList);
            }
            return appPermissionList;
        }

        public async Task<int> InsertAsync(AppPermission appPermission)
        {
            await _repository.AddAsync(appPermission);
            await _distributedCache.RemoveAsync(AppPermissionCacheKeys.ListKey);
            return appPermission.Id;
        }

        public async Task UpdateAsync(AppPermission appPermission)
        {
            await _repository.UpdateAsync(appPermission);
            await _distributedCache.RemoveAsync(AppPermissionCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(AppPermissionCacheKeys.GetKey(appPermission.Id));
        }
    }
}