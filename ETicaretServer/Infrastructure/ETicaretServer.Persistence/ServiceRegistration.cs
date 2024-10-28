using ETicaretServer.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretServer.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<IProductService, ProductService>();
            services.AddDbContext<ETicaretAPIDbContext>(options =>
            {
                options
                .UseNpgsql(configuration.GetConnectionString("PostgreSQL"));
            });
        }
    }
}
