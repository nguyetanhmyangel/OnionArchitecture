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
    public class KnowledgeBaseRepository : IKnowledgeBaseRepository
    {
        private readonly IRepositoryAsync<KnowledgeBase> _repository;
        private readonly IDistributedCache _distributedCache;

        public KnowledgeBaseRepository(IDistributedCache distributedCache, IRepositoryAsync<KnowledgeBase> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<KnowledgeBase> KnowledgeBases => _repository.Entities;

        public async Task DeleteAsync(KnowledgeBase knowledgeBase)
        {
            await _repository.DeleteAsync(knowledgeBase);
            await _distributedCache.RemoveAsync(KnowledgeBaseCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(KnowledgeBaseCacheKeys.GetKey(knowledgeBase.Id));
        }

        public async Task<KnowledgeBase> GetByIdAsync(int knowledgeBaseId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = KnowledgeBaseCacheKeys.GetKey(knowledgeBaseId);
            var knowledgeBase = await _distributedCache.GetAsync<KnowledgeBase>(cacheKey);
            if (knowledgeBase == null)
            {
                knowledgeBase = await _repository.Entities.Where(p => p.Id == knowledgeBaseId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(knowledgeBase, "MySpace", "No MySpace Found");
                await _distributedCache.SetAsync(cacheKey, knowledgeBase);
            }
            return knowledgeBase;
        }

        public async Task<List<KnowledgeBase>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = KnowledgeBaseCacheKeys.ListKey;
            var knowledgeBaseList = await _distributedCache.GetAsync<List<KnowledgeBase>>(cacheKey);
            if (knowledgeBaseList == null)
            {
                knowledgeBaseList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, knowledgeBaseList);
            }
            return knowledgeBaseList;
        }

        public async Task<int> InsertAsync(KnowledgeBase myBase)
        {
            await _repository.AddAsync(myBase);
            await _distributedCache.RemoveAsync(KnowledgeBaseCacheKeys.ListKey);
            return myBase.Id;
        }

        public async Task UpdateAsync(KnowledgeBase myBase)
        {
            await _repository.UpdateAsync(myBase);
            await _distributedCache.RemoveAsync(KnowledgeBaseCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(KnowledgeBaseCacheKeys.GetKey(myBase.Id));
        }
    }
}