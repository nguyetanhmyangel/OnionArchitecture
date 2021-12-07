using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IEnjoinFunctionRepository
    {
        IQueryable<EnjoinFunction> EnjoinFunctions { get; }

        Task<List<EnjoinFunction>> GetListAsync();

        Task<EnjoinFunction> GetByIdAsync(int enjoinId);

        Task<int> InsertAsync(EnjoinFunction enjoin);

        Task UpdateAsync(EnjoinFunction enjoin);

        Task DeleteAsync(EnjoinFunction enjoin);
    }
}