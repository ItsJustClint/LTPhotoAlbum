using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using AutoFixture;
using AutoFixture.AutoMoq;
using LTPhotoAlbum.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// You imported Moq here, but didn't use it.  I THINK though... given feedback below, you may want to use it now.  :)  HINT!
using Moq;
using Xunit;

namespace LTPhotoAlbum.Test
{
    public class PhotoRepositoryTest : UnitTestBase
    {
        // Note:  Does it make sense to perhaps test what happens when we get an error or timeout back from the webservice?
        // If we're not hitting the actual service in our test, we can simulate the failure coming back and then test our
        // error handling.  :)
        private readonly Fixture _fixture;
        private readonly Random _random;
        private readonly PhotoRepository _photoRepository;

        public PhotoRepositoryTest()
        {
            _fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());
            _random = new Random();

            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient();

            _photoRepository = new PhotoRepository(new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json")
                .Build(), services.BuildServiceProvider().GetService<IHttpClientFactory>());
        }

        [Fact]
        public async void PhotosReturnedAsync()
        {
            // NOTE: This function seems to be acting as an integration test as opposed to a unit test;
            // the GetPhotosAsync function here is making the actual service call out to the web.  You may want to
            // avoid that, such that your test is deterministic and isolated to the functionality within your own
            // "unit" of logic instead of testing the webservice.  This is for all your calls in this class.
            List<PhotoDto> photos = (List<PhotoDto>) await _photoRepository.GetPhotosAsync(null);

            Assert.True(photos.Count > 0);
        }

        [Fact]
        public async void NoPhotosReturnedAsync()
        {
            List<PhotoDto> photos = (List<PhotoDto>) await _photoRepository.GetPhotosAsync(500);

            Assert.True(photos.Count == 0);
        }

        [Fact]
        public async void NoNonFilteredPhotosReturnedAsync()
        {
            int albumId = _random.Next(1, 100);

            List<PhotoDto> photos = (List<PhotoDto>) await _photoRepository.GetPhotosAsync(albumId);

            Assert.False(photos.Exists(p => p.AlbumId != albumId));
        }

        [Fact]
        public async void AlbumsReturnedAsync()
        {
            Assert.True(((List<int>) await _photoRepository.GetAlbumIdsAsync()).Count > 0);
        }

        [Fact]
        public async void AlbumReturnMatchesActualAblumIdsAsync()
        {
            List<int> albumIds = (List<int>)await _photoRepository.GetAlbumIdsAsync();
            List<PhotoDto> photos = (List<PhotoDto>)await _photoRepository.GetPhotosAsync(null);

            Assert.Equal<IEnumerable<int>>((IEnumerable<int>)albumIds, (IEnumerable<int>)(photos.Select(s => s.AlbumId).Distinct().ToList()));
        }
    }
}
