using AgendaApp.Application.Contacts.Validations;
using AgendaApp.Domain.Abstractions;
using AgendaApp.Infrastructure.Context;
using AgendaApp.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System.Data;
using System.Reflection;


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

            services.AddSingleton<IDbConnection>(provider =>
            {
                var connection = new MySqlConnection(mySqlConnection);
                connection.Open();
                return connection;
            });

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IContactDapperRepository, ContactDapperRepository>();

            var myhandlers = AppDomain.CurrentDomain.Load("AgendaAPP.Application");
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(myhandlers);
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.Load("AgendaAPP.Application"));


            return services;
        }
    }
}
