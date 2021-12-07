using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;


namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IEnjoinRepository
    {
        IQueryable<Enjoin> Enjoins { get; }

        Task<List<Enjoin>> GetListAsync();

        Task<Enjoin> GetByIdAsync(int enjoinId);

        Task<int> InsertAsync(Enjoin enjoin);

        Task UpdateAsync(Enjoin enjoin);

        Task DeleteAsync(Enjoin enjoin);
    }
}