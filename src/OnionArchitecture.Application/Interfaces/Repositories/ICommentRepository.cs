using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }

        Task<List<Comment>> GetListAsync();

        Task<Comment> GetByIdAsync(int commentId);

        Task<int> InsertAsync(Comment comment);

        Task UpdateAsync(Comment comment);

        Task DeleteAsync(Comment comment);
    }
}