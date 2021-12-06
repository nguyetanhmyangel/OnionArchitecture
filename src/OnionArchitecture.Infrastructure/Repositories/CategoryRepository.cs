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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepositoryAsync<Category> _repository;
        private readonly IDistributedCache _distributedCache;

        public CategoryRepository(IDistributedCache distributedCache, IRepositoryAsync<Category> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Category> Categories => _repository.Entities;

        public async Task DeleteAsync(Category category)
        {
            await _repository.DeleteAsync(category);
            await _distributedCache.RemoveAsync(CategoryCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CategoryCacheKeys.GetKey(category.Id));
        }

        public async Task<Category> GetByIdAsync(int categoryId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = CategoryCacheKeys.GetKey(categoryId);
            var category = await _distributedCache.GetAsync<Category>(cacheKey);
            if (category == null)
            {
                category = await _repository.Entities.Where(p => p.Id == categoryId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(category, "Category", "No Category Found");
                await _distributedCache.SetAsync(cacheKey, category);
            }
            return category;
        }

        public async Task<List<Category>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = CategoryCacheKeys.ListKey;
            var categoryList = await _distributedCache.GetAsync<List<Category>>(cacheKey);
            if (categoryList == null)
            {
                categoryList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, categoryList);
            }
            return categoryList;
        }

        public async Task<int> InsertAsync(Category category)
        {
            await _repository.AddAsync(category);
            await _distributedCache.RemoveAsync(CategoryCacheKeys.ListKey);
            return category.Id;
        }

        public async Task UpdateAsync(Category category)
        {
            await _repository.UpdateAsync(category);
            await _distributedCache.RemoveAsync(CategoryCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CategoryCacheKeys.GetKey(category.Id));
        }
    }
}