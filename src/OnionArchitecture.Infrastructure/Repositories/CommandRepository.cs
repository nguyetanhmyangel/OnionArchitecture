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
    public class CommandRepository : ICommandRepository
    {
        private readonly IRepositoryAsync<Command> _repository;
        private readonly IDistributedCache _distributedCache;

        public CommandRepository(IDistributedCache distributedCache, IRepositoryAsync<Command> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Command> Commands => _repository.Entities;

        public async Task DeleteAsync(Command command)
        {
            await _repository.DeleteAsync(command);
            await _distributedCache.RemoveAsync(CommandCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CommandCacheKeys.GetKey(command.Id));
        }

        public async Task<Command> GetByIdAsync(int commandId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = CommandCacheKeys.GetKey(commandId);
            var command = await _distributedCache.GetAsync<Command>(cacheKey);
            if (command == null)
            {
                command = await _repository.Entities.Where(p => p.Id == commandId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(command, "Command", "No Command Found");
                await _distributedCache.SetAsync(cacheKey, command);
            }
            return command;
        }

        public async Task<List<Command>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = CommandCacheKeys.ListKey;
            var commandList = await _distributedCache.GetAsync<List<Command>>(cacheKey);
            if (commandList == null)
            {
                commandList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, commandList);
            }
            return commandList;
        }

        public async Task<int> InsertAsync(Command command)
        {
            await _repository.AddAsync(command);
            await _distributedCache.RemoveAsync(CommandCacheKeys.ListKey);
            return command.Id;
        }

        public async Task UpdateAsync(Command command)
        {
            await _repository.UpdateAsync(command);
            await _distributedCache.RemoveAsync(CommandCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CommandCacheKeys.GetKey(command.Id));
        }
    }
}