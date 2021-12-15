using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface ILabelKnowledgeBaseRepository
    {
        IQueryable<LabelKnowledgeBase> LabelKnowledgeBases { get; }

        Task<List<LabelKnowledgeBase>> GetListAsync();

        Task<LabelKnowledgeBase> GetByIdAsync(int labelKnowledgeBaseId);

        Task<int> InsertAsync(LabelKnowledgeBase labelKnowledgeBase);

        Task UpdateAsync(LabelKnowledgeBase labelKnowledgeBase);

        Task DeleteAsync(LabelKnowledgeBase labelKnowledgeBase);
    }
}