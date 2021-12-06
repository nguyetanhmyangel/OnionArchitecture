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
    public class FunctionRepository : IFunctionRepository
    {
        private readonly IRepositoryAsync<Function> _repository;
        private readonly IDistributedCache _distributedCache;

        public FunctionRepository(IDistributedCache distributedCache, IRepositoryAsync<Function> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Function> Functions => _repository.Entities;

        public async Task DeleteAsync(Function command)
        {
            await _repository.DeleteAsync(command);
            await _distributedCache.RemoveAsync(FunctionCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(FunctionCacheKeys.GetKey(command.Id));
        }

        public async Task<Function> GetByIdAsync(int commandId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = FunctionCacheKeys.GetKey(commandId);
            var command = await _distributedCache.GetAsync<Function>(cacheKey);
            if (command == null)
            {
                command = await _repository.Entities.Where(p => p.Id == commandId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(command, "Function", "No Function Found");
                await _distributedCache.SetAsync(cacheKey, command);
            }
            return command;
        }

        public async Task<List<Function>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = FunctionCacheKeys.ListKey;
            var functionList = await _distributedCache.GetAsync<List<Function>>(cacheKey);
            if (functionList == null)
            {
                functionList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, functionList);
            }
            return functionList;
        }

        public async Task<int> InsertAsync(Function function)
        {
            await _repository.AddAsync(function);
            await _distributedCache.RemoveAsync(FunctionCacheKeys.ListKey);
            return function.Id;
        }

        public async Task UpdateAsync(Function function)
        {
            await _repository.UpdateAsync(function);
            await _distributedCache.RemoveAsync(FunctionCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(FunctionCacheKeys.GetKey(function.Id));
        }
    }
}