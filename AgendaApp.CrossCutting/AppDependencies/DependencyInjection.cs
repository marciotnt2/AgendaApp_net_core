using AgendaApp.Domain.Abstractions;
using AgendaApp.Infrastructure.Context;
using AgendaApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System.Data;


namespace AgendaApp.CrossCutting.AppDependencies
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
                  this IServiceCollection services,
                  IConfiguration configuration)
        {
            var mySqlConnection = configuration
                                  .GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                             options.UseMySql(mySqlConnection,
                             ServerVersion.AutoDetect(mySqlConnection)));

            // Registrar IDbConnection como uma instância única
            services.AddSingleton<IDbConnection>(provider =>
            {
                var connection = new MySqlConnection(mySqlConnection);
                connection.Open();
                return connection;
            });

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
  

         
            return services;
        }
    }
}
