using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IVoteRepository
    {
        IQueryable<Vote> Votes { get; }

        Task<List<Vote>> GetListAsync();

        Task<Vote> GetByIdAsync(int voteId);

        Task<int> InsertAsync(Vote vote);

        Task UpdateAsync(Vote vote);

        Task DeleteAsync(Vote vote);
    }
}