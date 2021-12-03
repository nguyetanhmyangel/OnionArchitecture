using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IMyBaseRepository
    {
        IQueryable<MyBase> MyBases { get; }

        Task<List<MyBase>> GetListAsync();

        Task<MyBase> GetByIdAsync(int myBaseId);

        Task<int> InsertAsync(MyBase myBase);

        Task UpdateAsync(MyBase myBase);

        Task DeleteAsync(MyBase myBase);
    }
}