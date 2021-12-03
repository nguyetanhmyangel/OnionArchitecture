using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IFunctionRepository
    {
        IQueryable<Function> Functions { get; }

        Task<List<Function>> GetListAsync();

        Task<Function> GetByIdAsync(int functionId);

        Task<int> InsertAsync(Function function);

        Task UpdateAsync(Function function);

        Task DeleteAsync(Function function);
    }
}