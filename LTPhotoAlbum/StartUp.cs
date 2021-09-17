using LTPhotoAlbum.Repositories;
using LTPhotoAlbum.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace LTPhotoAlbum
{
    public class StartUp
    {
        private static IConfiguration _config;

        public static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            _config = LoadConfiguration();
            services.AddSingleton(_config);
            services.AddHttpClient();
            services.AddTransient<IPhotoAlbumDataRepository, PhotoAlbumDataRepository>();
            services.AddTransient<IPhotoRepository, PhotoRepository>();
            services.AddTransient<IAlbumRepository, AlbumRepository>();

            return services;
        }

        private static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
