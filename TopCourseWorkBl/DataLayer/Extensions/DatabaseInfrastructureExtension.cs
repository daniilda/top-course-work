using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TopCourseWorkBl.DataLayer.Extensions
{
    public static class DatabaseInfrastructureExtension
    {
        public static IServiceCollection AddDatabaseInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
             services
                .AddSingleton<IDbConnectionFactory, DbConnectionFactory>()
                .AddSingleton<MainRepository>()
                .AddSingleton<DbExecuteWrapper>();

             return services.AddFluentMigratorCore()
                .ConfigureRunner(x
                    => x.AddPostgres()
                        .WithGlobalConnectionString(configuration.GetPostgresConnectionString())
                        .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(y => y.AddFluentMigratorConsole());
        }
    }
}