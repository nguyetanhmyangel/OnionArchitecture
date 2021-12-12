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
        DbSet<AppCommand> AppCommands { get; set; }
        DbSet<AppCommandFunction> AppCommandFunctions { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Function> Functions { get; set; }
        DbSet<MySpace> KnowledgeBases { get; set; }
        DbSet<Label> Labels { get; set; }
        DbSet<LabelMySpace> LabelKnowledgeBases { get; set; }
        DbSet<AppPermission> Privileges { get; set; }
        DbSet<Report> Reports { get; set; }
        DbSet<Vote> Votes { get; set; }
    }
}