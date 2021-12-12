using System.Reflection;
//using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Interfaces.Contexts;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Infrastructure.Contexts;
using OnionArchitecture.Infrastructure.Repositories;

namespace OnionArchitecture.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAttachmentRepository, AttachmentRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IAppCommandFunctionRepository, AppCommandFunctionRepository>();
            services.AddTransient<IAppCommandRepository, AppCommandRepository>();
            services.AddTransient<IFunctionRepository, FunctionRepository>();
            services.AddTransient<ILabelMySpaceRepository, LabelMySpaceRepository>();
            services.AddTransient<ILabelRepository, LabelRepository>();
            services.AddTransient<IMySpaceRepository, MySpaceRepository>();
            services.AddTransient<IAppPermissionRepository, AppPermissionRepository>();
            services.AddTransient<IReportRepository, ReportRepository>();
            services.AddTransient<IVoteRepository, VoteRepository>();

            //services.AddTransient<IProductCacheRepository, ProductCacheRepository>();
            //services.AddTransient<IBrandCacheRepository, BrandCacheRepository>();

            #endregion Repositories
        }
    }
}