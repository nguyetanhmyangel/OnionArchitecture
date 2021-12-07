using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IMySpaceRepository
    {
        IQueryable<MySpace> MySpaces { get; }

        Task<List<MySpace>> GetListAsync();

        Task<MySpace> GetByIdAsync(int mySpaceId);

        Task<int> InsertAsync(MySpace mySpace);

        Task UpdateAsync(MySpace mySpace);

        Task DeleteAsync(MySpace mySpace);
    }
}