using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface ILabelMyBaseRepository
    {
        IQueryable<LabelMyBase> LabelMyBases { get; }

        Task<List<LabelMyBase>> GetListAsync();

        Task<LabelMyBase> GetByIdAsync(int labelMyBaseId);

        Task<int> InsertAsync(LabelMyBase labelMyBase);

        Task UpdateAsync(LabelMyBase labelMyBase);

        Task DeleteAsync(LabelMyBase labelMyBase);
    }
}