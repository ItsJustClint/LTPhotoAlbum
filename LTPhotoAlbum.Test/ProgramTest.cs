using LTPhotoAlbum.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Xunit;

namespace LTPhotoAlbum.Test
{
    public class ProgramTest
    {

        private readonly Random _random;
        private readonly PhotoRepository _photoRepository;

        public ProgramTest()
        {
            _random = new Random();
             
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient();

            _photoRepository = new PhotoRepository(new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json")
                .Build(), services.BuildServiceProvider().GetService<IHttpClientFactory>());
        }

        [Fact]
        public async void ValidAblumIdAsync()
        {
            int albumId = _random.Next(1, 100);

            Assert.True(string.IsNullOrEmpty(Program.IsValidAlbumId(albumId.ToString(), (List<int>)await _photoRepository.GetAlbumIdsAsync())));
        }

        [Fact]
        public async void InvalidAlbumId_NumericAsync()
        {
            int albumId = _random.Next(101, 1000);

            Assert.False(string.IsNullOrEmpty(Program.IsValidAlbumId(albumId.ToString(), (List<int>)await _photoRepository.GetAlbumIdsAsync())));
        }

        [Fact]
        public async void InvalidAlbumId_NonNumericAsync()
        {
            Assert.False(string.IsNullOrEmpty(Program.IsValidAlbumId("InvalidAlbumId", (List<int>)await _photoRepository.GetAlbumIdsAsync())));
        }
    }
}
