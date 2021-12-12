using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IAppPermissionRepository
    {
        IQueryable<AppPermission> AppPermission { get; }

        Task<List<AppPermission>> GetListAsync();

        Task<AppPermission> GetByIdAsync(int appPermissionId);

        Task<int> InsertAsync(AppPermission appPermission);

        Task UpdateAsync(AppPermission appPermission);

        Task DeleteAsync(AppPermission appPermission);
    }
}