using GSAExam.Infrastructure.Interface;
using GSAExam.Infrastructure.Persistence;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace GSAExam.Infrastructure
{
    public static class InfrastructureDependencyExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("DefaultConnection");
            services.TryAddSingleton(new SqlServerStorageOptions());
            services.AddDbContext<GSADbContext>(options =>
               options.UseSqlServer(conn,
                   b => b.MigrationsAssembly(typeof(GSADbContext).Assembly.FullName)));
            services.AddScoped<IGSADbContext>(provider => provider.GetService<GSADbContext>());
            //services.AddHangfire((provider, configuration) => configuration
            //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseSqlServerStorage(
            //        conn,
            //        provider.GetRequiredService<SqlServerStorageOptions>())
            //    );
            return services;
        }
    }
}
