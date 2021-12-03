using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IReportRepository
    {
        IQueryable<Report> Reports { get; }

        Task<List<Report>> GetListAsync();

        Task<Report> GetByIdAsync(int reportId);

        Task<int> InsertAsync(Report report);

        Task UpdateAsync(Report report);

        Task DeleteAsync(Report report);
    }
}