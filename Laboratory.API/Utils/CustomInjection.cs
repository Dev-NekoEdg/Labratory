using Labratory.Domain.Configs;
using Labratory.Infrastructure.Repositories;
using Labratory.Service.BlobStorage;
using Labratory.Service.IRepositories;
using Labratory.Service.ListItems;
using Labratory.Service.Lists;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.API.Utils
{
    public static class CustomInjection
    {

        public static IServiceCollection AddProjectTransient(this IServiceCollection services) 
        {

            services.AddTransient<IListsRepository, ListsRepository>();
            services.AddTransient<IListItemRepository, ListItemRepository>();

            services.AddTransient<IListsService, ListsService>();
            services.AddTransient<IListItemService, ListItemService>();
            
            services.AddTransient<IBlobStorageService, BlobStorageService>();

            return services; 
        }

        public static IServiceCollection AddProjectConfiguration(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<BlobContainerConfig>(configuration.GetSection("BlobStorageConfig"));
            services.Configure<CommonConfig>(configuration.GetSection("CommonConfig"));
            
            return services;
        }

    }
}
