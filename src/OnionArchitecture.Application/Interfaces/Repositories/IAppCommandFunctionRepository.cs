using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IAppCommandFunctionRepository
    {
        IQueryable<AppCommandFunction> AppCommandFunctions { get; }

        Task<List<AppCommandFunction>> GetListAsync();

        Task<AppCommandFunction> GetByIdAsync(int appCommandId);

        Task<int> InsertAsync(AppCommandFunction appCommand);

        Task UpdateAsync(AppCommandFunction appCommand);

        Task DeleteAsync(AppCommandFunction appCommand);
    }
}