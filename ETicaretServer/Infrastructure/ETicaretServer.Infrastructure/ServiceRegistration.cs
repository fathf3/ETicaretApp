using ETicaretServer.Application.Services;
using ETicaretServer.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretServer.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
        }

    }
}
