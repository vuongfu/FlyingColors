using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StartUpApp.Repository.Abstract;
using StartUpApp.Repository.Concret;
using StartUpApp.Service.Abstract;
using StartUpApp.Service.Concret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartUpApp.Web.UI.Infrastructure
{
    public static class StartUpExtensions
    {
        public static IServiceCollection AddStartupExtensions(this IServiceCollection services, IConfigurationRoot configuration)
        {
            #region Repositories

            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();

            #endregion

            #region Services

            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<ICategoryService, CategoryService>();

            #endregion
            return services;
        }
    }
}
