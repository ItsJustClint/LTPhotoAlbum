using AutoFixture;
using AutoFixture.AutoMoq;
using LTPhotoAlbum.Repositories.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LTPhotoAlbum.Test
{
    public class PhotoAlbumDataRepositoryTest
    {

        private readonly Random _random;
        private readonly Mock<IPhotoAlbumDataRepository> _mockPhotoAlbumDataRepository;
        private readonly IEnumerable<PhotoDto> _serviceRecords;

        public PhotoAlbumDataRepositoryTest()
        {
            Fixture fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());

            _random = new Random();

            _serviceRecords = fixture.CreateMany<PhotoDto>();

            _mockPhotoAlbumDataRepository = new Mock<IPhotoAlbumDataRepository>();
        }

        [Fact]
        public async void DataReturnedAsync()
        {
            _mockPhotoAlbumDataRepository.Setup(s => s.GetDataAsync(null)).ReturnsAsync(_serviceRecords);

            IEnumerable<PhotoDto> photos = await _mockPhotoAlbumDataRepository.Object.GetDataAsync(null);

            Assert.NotEmpty(photos);
        }

        [Fact]
        public async void OutputMatchesExpectedAsync()
        {
            _mockPhotoAlbumDataRepository.Setup(s => s.GetDataAsync(null)).ReturnsAsync(_serviceRecords);

            IEnumerable<PhotoDto> photos = await _mockPhotoAlbumDataRepository.Object.GetDataAsync(null);

            Assert.Equal(_serviceRecords, photos);
        }

        [Fact]
        public async void NoDataReturnedAsync()
        {
            int albumId = _random.Next();

            while (_serviceRecords.Any(a => a.AlbumId == albumId))
            {
                albumId = _random.Next();
            }

            _mockPhotoAlbumDataRepository.Setup(s => s.GetDataAsync(albumId)).ReturnsAsync(_serviceRecords.Where(w => w.AlbumId == albumId));

            IEnumerable<PhotoDto> photos = await _mockPhotoAlbumDataRepository.Object.GetDataAsync(albumId);

            Assert.Empty(photos);
        }

        [Fact]
        public async void NoNonFilteredDataReturnedAsync()
        {
            int indexToUse = _random.Next(1, 3);

            int albumId = _serviceRecords.ElementAt(indexToUse).AlbumId;

            _mockPhotoAlbumDataRepository.Setup(s => s.GetDataAsync(albumId)).ReturnsAsync(_serviceRecords.Where(w => w.AlbumId == albumId));

            IEnumerable<PhotoDto> photos = await _mockPhotoAlbumDataRepository.Object.GetDataAsync(albumId);

            Assert.DoesNotContain(photos, a => a.AlbumId != albumId);
        }

        [Fact]
        public async Task HttpRequestExceptionThrown()
        {
            _mockPhotoAlbumDataRepository.Setup(s => s.GetDataAsync(null)).Throws(new HttpRequestException());

            await Assert.ThrowsAsync<HttpRequestException>(() => _mockPhotoAlbumDataRepository.Object.GetDataAsync(null));
        }
    }
}
