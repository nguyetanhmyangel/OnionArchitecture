﻿//using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Interfaces.Contexts;
using OnionArchitecture.Infrastructure.Contexts;

namespace OnionArchitecture.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            //services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            //services.AddTransient<IProductRepository, ProductRepository>();
            //services.AddTransient<IProductCacheRepository, ProductCacheRepository>();
            //services.AddTransient<IBrandRepository, BrandRepository>();
            //services.AddTransient<IBrandCacheRepository, BrandCacheRepository>();
            //services.AddTransient<ILogRepository, LogRepository>();
            //services.AddTransient<IUnitOfWork, UnitOfWork>();

            #endregion Repositories
        }
    }
}