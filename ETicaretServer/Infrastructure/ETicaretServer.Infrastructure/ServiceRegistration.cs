﻿using ETicaretServer.Application.Abstractions.Services;
using ETicaretServer.Application.Abstractions.Storage;
using ETicaretServer.Application.Abstractions.Token;
using ETicaretServer.Application.Configurations;
using ETicaretServer.Infrastructure.Enums;
using ETicaretServer.Infrastructure.Services.Configurations;
using ETicaretServer.Infrastructure.Services.Mail;
using ETicaretServer.Infrastructure.Services.Storage;
using ETicaretServer.Infrastructure.Services.Storage.Local;
using ETicaretServer.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretServer.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IApplicationService, ApplicationService>();

        }

        public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
        public static void AddStorage<T>(this IServiceCollection services, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    //services.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:
                    //services.AddScoped<IStorage, AWSStorage>();
                    break;
                default:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
