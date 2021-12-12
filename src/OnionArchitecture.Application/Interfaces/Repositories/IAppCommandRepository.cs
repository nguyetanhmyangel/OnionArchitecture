using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;


namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IAppCommandRepository
    {
        IQueryable<AppCommand> AppCommands { get; }

        Task<List<AppCommand>> GetListAsync();

        Task<AppCommand> GetByIdAsync(int appCommandId);

        Task<int> InsertAsync(AppCommand appCommand);

        Task UpdateAsync(AppCommand appCommand);

        Task DeleteAsync(AppCommand appCommand);
    }
}