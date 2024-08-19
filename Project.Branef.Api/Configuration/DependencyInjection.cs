using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Project.Branef.Application.Commands.v1.Customer.Create;
using Project.Branef.Domain.Interfaces.Repositories.v1.Customer;
using Project.Branef.Infrastructure.Data;
using Project.Branef.Infrastructure.Data.Repositories.v1.Custormer.MongoRepositorie;
using Project.Branef.Infrastructure.Data.Repositories.v1.Custormer.SqlRepositorie;

namespace Project.Branef.Api.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration Configuration)
        {
            //DataContext e repositórios SQL
            services.AddScoped<SQLDataContext>();
            services.AddScoped<ICustomerSqlRepository, CustomerSqlRepository>();
            services.AddScoped<ICustomerMongoRepository, CustomerMongoRepository>();

            //Configurações do Mongo
            services.Configure<MongoSettings>(Configuration.GetSection("MongoSettings"));
            services.AddSingleton<IMongoClient>(s =>
            {
                var settings = s.GetRequiredService<IOptions<MongoSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            }); services.AddScoped<IMongoDatabase>(s =>
            {
                var settings = s.GetRequiredService<IOptions<MongoSettings>>().Value;
                var client = s.GetRequiredService<IMongoClient>();
                return client.GetDatabase(settings.DatabaseName);
            });

            //Registro do MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateCustomerCommand)));

            return services;
        }
    }
}
