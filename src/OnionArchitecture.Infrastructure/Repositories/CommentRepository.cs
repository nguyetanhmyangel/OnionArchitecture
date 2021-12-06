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
    public class CommentRepository : ICommentRepository
    {
        private readonly IRepositoryAsync<Comment> _repository;
        private readonly IDistributedCache _distributedCache;

        public CommentRepository(IDistributedCache distributedCache, IRepositoryAsync<Comment> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Comment> Comments => _repository.Entities;

        public async Task DeleteAsync(Comment comment)
        {
            await _repository.DeleteAsync(comment);
            await _distributedCache.RemoveAsync(CommentCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CommentCacheKeys.GetKey(comment.Id));
        }

        public async Task<Comment> GetByIdAsync(int commentId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = CommentCacheKeys.GetKey(commentId);
            var comment = await _distributedCache.GetAsync<Comment>(cacheKey);
            if (comment == null)
            {
                comment = await _repository.Entities.Where(p => p.Id == commentId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(comment, "Comment", "No Comment Found");
                await _distributedCache.SetAsync(cacheKey, comment);
            }
            return comment;
        }

        public async Task<List<Comment>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = CommentCacheKeys.ListKey;
            var commentList = await _distributedCache.GetAsync<List<Comment>>(cacheKey);
            if (commentList == null)
            {
                commentList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, commentList);
            }
            return commentList;
        }

        public async Task<int> InsertAsync(Comment comment)
        {
            await _repository.AddAsync(comment);
            await _distributedCache.RemoveAsync(CommentCacheKeys.ListKey);
            return comment.Id;
        }

        public async Task UpdateAsync(Comment comment)
        {
            await _repository.UpdateAsync(comment);
            await _distributedCache.RemoveAsync(CommentCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CommentCacheKeys.GetKey(comment.Id));
        }
    }
}