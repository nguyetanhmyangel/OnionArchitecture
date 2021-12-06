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
    public class PrivilegeRepository : IPrivilegeRepository
    {
        private readonly IRepositoryAsync<Privilege> _repository;
        private readonly IDistributedCache _distributedCache;

        public PrivilegeRepository(IDistributedCache distributedCache, IRepositoryAsync<Privilege> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Privilege> Privileges => _repository.Entities;


        public async Task DeleteAsync(Privilege privilege)
        {
            await _repository.DeleteAsync(privilege);
            await _distributedCache.RemoveAsync(PrivilegeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(PrivilegeCacheKeys.GetKey(privilege.Id));
        }

        public async Task<Privilege> GetByIdAsync(int privilegeId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = PrivilegeCacheKeys.GetKey(privilegeId);
            var labelMyBase = await _distributedCache.GetAsync<Privilege>(cacheKey);
            if (labelMyBase == null)
            {
                labelMyBase = await _repository.Entities.Where(p => p.Id == privilegeId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(labelMyBase, "Comment", "No Comment Found");
                await _distributedCache.SetAsync(cacheKey, labelMyBase);
            }
            return labelMyBase;
        }

        public async Task<List<Privilege>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = PrivilegeCacheKeys.ListKey;
            var privilegeList = await _distributedCache.GetAsync<List<Privilege>>(cacheKey);
            if (privilegeList == null)
            {
                privilegeList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, privilegeList);
            }
            return privilegeList;
        }

        public async Task<int> InsertAsync(Privilege privilege)
        {
            await _repository.AddAsync(privilege);
            await _distributedCache.RemoveAsync(PrivilegeCacheKeys.ListKey);
            return privilege.Id;
        }

        public async Task UpdateAsync(Privilege privilege)
        {
            await _repository.UpdateAsync(privilege);
            await _distributedCache.RemoveAsync(PrivilegeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(PrivilegeCacheKeys.GetKey(privilege.Id));
        }
    }
}