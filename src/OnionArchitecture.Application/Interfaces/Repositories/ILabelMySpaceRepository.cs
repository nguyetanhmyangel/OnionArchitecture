using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface ILabelMySpaceRepository
    {
        IQueryable<LabelMySpace> LabelMySpaces { get; }

        Task<List<LabelMySpace>> GetListAsync();

        Task<LabelMySpace> GetByIdAsync(int labelMySpaceId);

        Task<int> InsertAsync(LabelMySpace labelMySpace);

        Task UpdateAsync(LabelMySpace labelMySpace);

        Task DeleteAsync(LabelMySpace labelMySpace);
    }
}