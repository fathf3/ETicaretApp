using ETicaretServer.Application.Abstractions.Hubs;
using ETicaretServer.SignalR.HubServices;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretServer.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddTransient<IProductHubService, ProductHubService>();
            services.AddTransient<IOrderHubService, OrderHubService>();
            services.AddSignalR();
        }
    }
}
