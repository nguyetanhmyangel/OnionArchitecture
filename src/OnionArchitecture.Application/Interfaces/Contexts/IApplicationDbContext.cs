using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnionArchitecture.Domain.Entities;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace OnionArchitecture.Application.Interfaces.Contexts
{
    public interface IApplicationDbContext
    {
        IDbConnection Connection { get; }
        bool HasChanges { get; }

        EntityEntry Entry(object entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<Attachment> Attachments { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Command> Commands { get; set; }
        DbSet<CommandFunction> CommandFunctions { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Function> Functions { get; set; }
        DbSet<KnowledgeBase> KnowledgeBases { get; set; }
        DbSet<Label> Labels { get; set; }
        DbSet<LabelMyBase> LabelKnowledgeBases { get; set; }
        DbSet<Privilege> Privileges { get; set; }
        DbSet<Report> Reports { get; set; }
        DbSet<Vote> Votes { get; set; }
    }
}