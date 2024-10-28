using ETicaretServer.Application.Repositories;
using ETicaretServer.Persistence.Contexts;
using ETicaretServer.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretServer.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ETicaretAPIDbContext>(options =>
            {
                options
                .UseNpgsql(configuration.GetConnectionString("PostgreSQL"));
                
            }); 
            
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

           
        }
    }
}
