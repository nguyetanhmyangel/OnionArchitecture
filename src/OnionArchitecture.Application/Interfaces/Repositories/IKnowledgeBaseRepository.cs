using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IKnowledgeBaseRepository
    {
        IQueryable<KnowledgeBase> KnowledgeBases { get; }

        Task<List<KnowledgeBase>> GetListAsync();

        Task<KnowledgeBase> GetByIdAsync(int knowledgeBaseId);

        Task<int> InsertAsync(KnowledgeBase knowledgeBase);

        Task UpdateAsync(KnowledgeBase knowledgeBase);

        Task DeleteAsync(KnowledgeBase knowledgeBase);
    }
}