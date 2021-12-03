using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface ILabelRepository
    {
        IQueryable<Label> Labels { get; }

        Task<List<Label>> GetListAsync();

        Task<Label> GetByIdAsync(int labelId);

        Task<int> InsertAsync(Label label);

        Task UpdateAsync(Label label);

        Task DeleteAsync(Label label);
    }
}