using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface ICommandFunctionRepository
    {
        IQueryable<CommandFunction> CommandFunctions { get; }

        Task<List<CommandFunction>> GetListAsync();

        Task<CommandFunction> GetByIdAsync(int commandFunctionId);

        Task<int> InsertAsync(CommandFunction commandFunction);

        Task UpdateAsync(CommandFunction commandFunction);

        Task DeleteAsync(CommandFunction commandFunction);
    }
}