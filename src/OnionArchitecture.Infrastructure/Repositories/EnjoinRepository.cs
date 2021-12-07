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
    public class EnjoinRepository : IEnjoinRepository
    {
        private readonly IRepositoryAsync<Enjoin> _repository;
        private readonly IDistributedCache _distributedCache;

        public EnjoinRepository(IDistributedCache distributedCache, IRepositoryAsync<Enjoin> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Enjoin> Enjoins => _repository.Entities;

        public async Task DeleteAsync(Enjoin enjoin)
        {
            await _repository.DeleteAsync(enjoin);
            await _distributedCache.RemoveAsync(EnjoinCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(EnjoinCacheKeys.GetKey(enjoin.Id));
        }

        public async Task<Enjoin> GetByIdAsync(int enjoinId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = EnjoinCacheKeys.GetKey(enjoinId);
            var enjoin = await _distributedCache.GetAsync<Enjoin>(cacheKey);
            if (enjoin == null)
            {
                enjoin = await _repository.Entities.Where(p => p.Id == enjoinId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(enjoin, "Command", "No Command Found");
                await _distributedCache.SetAsync(cacheKey, enjoin);
            }
            return enjoin;
        }

        public async Task<List<Enjoin>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = EnjoinCacheKeys.ListKey;
            var enjoinList = await _distributedCache.GetAsync<List<Enjoin>>(cacheKey);
            if (enjoinList == null)
            {
                enjoinList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, enjoinList);
            }
            return enjoinList;
        }

        public async Task<int> InsertAsync(Enjoin enjoin)
        {
            await _repository.AddAsync(enjoin);
            await _distributedCache.RemoveAsync(EnjoinCacheKeys.ListKey);
            return enjoin.Id;
        }

        public async Task UpdateAsync(Enjoin enjoin)
        {
            await _repository.UpdateAsync(enjoin);
            await _distributedCache.RemoveAsync(EnjoinCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(EnjoinCacheKeys.GetKey(enjoin.Id));
        }
    }
}