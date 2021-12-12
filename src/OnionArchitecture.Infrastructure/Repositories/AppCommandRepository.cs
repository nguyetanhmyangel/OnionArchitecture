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
    public class AppCommandRepository : IAppCommandRepository
    {
        private readonly IRepositoryAsync<AppCommand> _repository;
        private readonly IDistributedCache _distributedCache;

        public AppCommandRepository(IDistributedCache distributedCache, IRepositoryAsync<AppCommand> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<AppCommand> AppCommands => _repository.Entities;

        public async Task DeleteAsync(AppCommand appCommand)
        {
            await _repository.DeleteAsync(appCommand);
            await _distributedCache.RemoveAsync(AppCommandCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(AppCommandCacheKeys.GetKey(appCommand.Id));
        }

        public async Task<AppCommand> GetByIdAsync(int appCommandId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = AppCommandCacheKeys.GetKey(appCommandId);
            var appCommand = await _distributedCache.GetAsync<AppCommand>(cacheKey);
            if (appCommand == null)
            {
                appCommand = await _repository.Entities.Where(p => p.Id == appCommandId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(appCommand, "appCommand", "No appCommand Found");
                await _distributedCache.SetAsync(cacheKey, appCommand);
            }
            return appCommand;
        }

        public async Task<List<AppCommand>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = AppCommandCacheKeys.ListKey;
            var appCommandList = await _distributedCache.GetAsync<List<AppCommand>>(cacheKey);
            if (appCommandList == null)
            {
                appCommandList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, appCommandList);
            }
            return appCommandList;
        }

        public async Task<int> InsertAsync(AppCommand appCommand)
        {
            await _repository.AddAsync(appCommand);
            await _distributedCache.RemoveAsync(AppCommandCacheKeys.ListKey);
            return appCommand.Id;
        }

        public async Task UpdateAsync(AppCommand appCommand)
        {
            await _repository.UpdateAsync(appCommand);
            await _distributedCache.RemoveAsync(AppCommandCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(AppCommandCacheKeys.GetKey(appCommand.Id));
        }
    }
}