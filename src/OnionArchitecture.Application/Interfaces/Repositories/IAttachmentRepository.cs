using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IAttachmentRepository
    {
        IQueryable<Attachment> Attachments { get; }

        Task<List<Attachment>> GetCachedListAsync();

        Task<List<Attachment>> GetListAsync();

        Task<Attachment> GetByIdAsync(int attachmentId);

        Task<Attachment> GetCachedByIdAsync(int attachmentId);

        Task<int> InsertAsync(Attachment attachmentId);

        Task UpdateAsync(Attachment attachmentId);

        Task DeleteAsync(Attachment attachmentId);
    }
}