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
    public class CommandFunctionRepository : ICommandFunctionRepository
    {
        private readonly IRepositoryAsync<CommandFunction> _repository;
        private readonly IDistributedCache _distributedCache;

        public CommandFunctionRepository(IDistributedCache distributedCache, IRepositoryAsync<CommandFunction> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<CommandFunction> CommandFunctions => _repository.Entities;

        public async Task DeleteAsync(CommandFunction commandFunction)
        {
            await _repository.DeleteAsync(commandFunction);
            await _distributedCache.RemoveAsync(CommandFunctionCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CommandFunctionCacheKeys.GetKey(commandFunction.Id));
        }

        public async Task<CommandFunction> GetByIdAsync(int commandFunctionId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = CommandFunctionCacheKeys.GetKey(commandFunctionId);
            var commandFunction = await _distributedCache.GetAsync<CommandFunction>(cacheKey);
            if (commandFunction == null)
            {
                commandFunction = await _repository.Entities.Where(p => p.Id == commandFunctionId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(commandFunction, "CommandFunction", "No CommandFunction Found");
                await _distributedCache.SetAsync(cacheKey, commandFunction);
            }
            return commandFunction;
        }

        public async Task<List<CommandFunction>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = CommandFunctionCacheKeys.ListKey;
            var commandFunctionList = await _distributedCache.GetAsync<List<CommandFunction>>(cacheKey);
            if (commandFunctionList == null)
            {
                commandFunctionList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, commandFunctionList);
            }
            return commandFunctionList;
        }

        public async Task<int> InsertAsync(CommandFunction commandFunction)
        {
            await _repository.AddAsync(commandFunction);
            await _distributedCache.RemoveAsync(CommandFunctionCacheKeys.ListKey);
            return commandFunction.Id;
        }

        public async Task UpdateAsync(CommandFunction commandFunction)
        {
            await _repository.UpdateAsync(commandFunction);
            await _distributedCache.RemoveAsync(CommandFunctionCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CommandFunctionCacheKeys.GetKey(commandFunction.Id));
        }
    }
}