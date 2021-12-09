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
    public class VoteRepository : IVoteRepository
    {
        private readonly IRepositoryAsync<Vote> _repository;
        private readonly IDistributedCache _distributedCache;

        public VoteRepository(IDistributedCache distributedCache, IRepositoryAsync<Vote> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Vote> Votes => _repository.Entities;

        public async Task DeleteAsync(Vote vote)
        {
            await _repository.DeleteAsync(vote);
            await _distributedCache.RemoveAsync(VoteCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(VoteCacheKeys.GetKey(vote.Id));
        }

        public async Task<Vote> GetByIdAsync(int voteId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = VoteCacheKeys.GetKey(voteId);
            var vote = await _distributedCache.GetAsync<Vote>(cacheKey);
            if (vote == null)
            {
                vote = await _repository.Entities.Where(p => p.Id == voteId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(vote, "Vote", "No Vote Found");
                await _distributedCache.SetAsync(cacheKey, vote);
            }
            return vote;
        }

        public async Task<List<Vote>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = VoteCacheKeys.ListKey;
            var voteList = await _distributedCache.GetAsync<List<Vote>>(cacheKey);
            if (voteList == null)
            {
                voteList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, voteList);
            }
            return voteList;
        }

        public async Task<int> InsertAsync(Vote vote)
        {
            await _repository.AddAsync(vote);
            await _distributedCache.RemoveAsync(VoteCacheKeys.ListKey);
            return vote.Id;
        }

        public async Task UpdateAsync(Vote vote)
        {
            await _repository.UpdateAsync(vote);
            await _distributedCache.RemoveAsync(VoteCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(VoteCacheKeys.GetKey(vote.Id));
        }
    }
}