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
    public class ReportRepository : IReportRepository
    {
        private readonly IRepositoryAsync<Report> _repository;
        private readonly IDistributedCache _distributedCache;

        public ReportRepository(IDistributedCache distributedCache, IRepositoryAsync<Report> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Report> Reports => _repository.Entities;

        public async Task DeleteAsync(Report report)
        {
            await _repository.DeleteAsync(report);
            await _distributedCache.RemoveAsync(ReportCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(ReportCacheKeys.GetKey(report.Id));
        }

        public async Task<Report> GetByIdAsync(int reportId)
        {
            //not use cache
            //return await _repository.Entities.Where(p => p.Id == CategoryId).FirstOrDefaultAsync();

            var cacheKey = ReportCacheKeys.GetKey(reportId);
            var report = await _distributedCache.GetAsync<Report>(cacheKey);
            if (report == null)
            {
                report = await _repository.Entities.Where(p => p.Id == reportId).FirstOrDefaultAsync();
                Throw.Exception.IfNull(report, "Comment", "No Comment Found");
                await _distributedCache.SetAsync(cacheKey, report);
            }
            return report;
        }

        public async Task<List<Report>> GetListAsync()
        {
            //not use cache
            //return await _repository.Entities.ToListAsync();

            var cacheKey = ReportCacheKeys.ListKey;
            var reportList = await _distributedCache.GetAsync<List<Report>>(cacheKey);
            if (reportList == null)
            {
                reportList = await _repository.Entities.ToListAsync();
                await _distributedCache.SetAsync(cacheKey, reportList);
            }
            return reportList;
        }

        public async Task<int> InsertAsync(Report report)
        {
            await _repository.AddAsync(report);
            await _distributedCache.RemoveAsync(ReportCacheKeys.ListKey);
            return report.Id;
        }

        public async Task UpdateAsync(Report report)
        {
            await _repository.UpdateAsync(report);
            await _distributedCache.RemoveAsync(ReportCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(ReportCacheKeys.GetKey(report.Id));
        }
    }
}