using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IPrivilegeRepository
    {
        IQueryable<Privilege> Privileges { get; }

        Task<List<Privilege>> GetListAsync();

        Task<Privilege> GetByIdAsync(int privilegeId);

        Task<int> InsertAsync(Privilege privilege);

        Task UpdateAsync(Privilege privilege);

        Task DeleteAsync(Privilege privilege);
    }
}