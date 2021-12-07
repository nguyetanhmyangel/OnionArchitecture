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
    public class EnjoinFunctionRepository : IEnjoinFunctionRepository
    {
        private readonly IRepositoryAsync<EnjoinFunction> _repository;
        private readonly IDistributedCache _distributedCache;

        public EnjoinFunctionRepository(IDistributedCache distributedCache, IRepositoryAsync<EnjoinFunction> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<EnjoinFunction> EnjoinFunctions => _repository.Entities;

        public async Task DeleteAsync(EnjoinFunction enjoinFunction)
        {
            await _repository.DeleteAsync(enjoinFunction);
            await _distributedCache.RemoveAsync(EnjoinFunctionCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(EnjoinFunctionCacheKeys.GetKey(enjoinFunction.Id));
        }

        public async Task<EnjoinFunction> GetByIdAsync(int enjoinFunctionId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = EnjoinFunctionCacheKeys.GetKey(enjoinFunctionId);
            var enjoinFunction = await _distributedCache.GetAsync<EnjoinFunction>(cacheKey);
            if (enjoinFunction == null)
            {
                enjoinFunction = await _repository.Entities.Where(p => p.Id == enjoinFunctionId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(enjoinFunction, "CommandFunction", "No CommandFunction Found");
                await _distributedCache.SetAsync(cacheKey, enjoinFunction);
            }
            return enjoinFunction;
        }

        public async Task<List<EnjoinFunction>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = EnjoinFunctionCacheKeys.ListKey;
            var enjoinFunctionList = await _distributedCache.GetAsync<List<EnjoinFunction>>(cacheKey);
            if (enjoinFunctionList == null)
            {
                enjoinFunctionList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, enjoinFunctionList);
            }
            return enjoinFunctionList;
        }

        public async Task<int> InsertAsync(EnjoinFunction enjoinFunction)
        {
            await _repository.AddAsync(enjoinFunction);
            await _distributedCache.RemoveAsync(EnjoinFunctionCacheKeys.ListKey);
            return enjoinFunction.Id;
        }

        public async Task UpdateAsync(EnjoinFunction enjoinFunction)
        {
            await _repository.UpdateAsync(enjoinFunction);
            await _distributedCache.RemoveAsync(EnjoinFunctionCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(EnjoinFunctionCacheKeys.GetKey(enjoinFunction.Id));
        }
    }
}