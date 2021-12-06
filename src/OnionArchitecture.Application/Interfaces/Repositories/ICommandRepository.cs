using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;


namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface ICommandRepository
    {
        IQueryable<Command> Commands { get; }

        Task<List<Command>> GetListAsync();

        Task<Command> GetByIdAsync(int commandId);

        Task<int> InsertAsync(Command command);

        Task UpdateAsync(Command command);

        Task DeleteAsync(Command command);
    }
}