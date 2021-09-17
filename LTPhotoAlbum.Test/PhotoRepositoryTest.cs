using AutoFixture;
using AutoFixture.AutoMoq;
using LTPhotoAlbum.Repositories.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LTPhotoAlbum.Test
{
    public class PhotoRepositoryTest
    {
        private readonly Random _random;
        private readonly Mock<IPhotoRepository> _mockPhotoRepository;
        private readonly IEnumerable<PhotoDto> _serviceRecords;

        public PhotoRepositoryTest()
        {
            Fixture fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());

            _random = new Random();

            _serviceRecords = fixture.CreateMany<PhotoDto>();

            _mockPhotoRepository = new Mock<IPhotoRepository>();
        }

        [Fact]
        public async void PhotosReturnedAsync()
        {
            _mockPhotoRepository.Setup(s => s.GetPhotosAsync(null)).ReturnsAsync(_serviceRecords);

            IEnumerable<PhotoDto> photos = await _mockPhotoRepository.Object.GetPhotosAsync(null);

            Assert.NotEmpty(photos);
        }

        [Fact]
        public async void OutputMatchesExpectedAsync()
        {
            _mockPhotoRepository.Setup(s => s.GetPhotosAsync(null)).ReturnsAsync(_serviceRecords);

            IEnumerable<PhotoDto> photos = await _mockPhotoRepository.Object.GetPhotosAsync(null);

            Assert.Equal(_serviceRecords, photos);
        }

        [Fact]
        public async void NoPhotosReturnedAsync()
        {
            int albumId = _random.Next();

            while (_serviceRecords.Any(a => a.AlbumId == albumId))
            {
                albumId = _random.Next();
            }

            _mockPhotoRepository.Setup(s => s.GetPhotosAsync(albumId)).ReturnsAsync(_serviceRecords.Where(w => w.AlbumId == albumId));

            IEnumerable<PhotoDto> photos = await _mockPhotoRepository.Object.GetPhotosAsync(albumId);

            Assert.Empty(photos);
        }

        [Fact]
        public async void NoNonFilteredPhotosReturnedAsync()
        {
            int indexToUse = _random.Next(1, 3);

            int albumId = _serviceRecords.ElementAt(indexToUse).AlbumId;

            _mockPhotoRepository.Setup(s => s.GetPhotosAsync(albumId)).ReturnsAsync(_serviceRecords.Where(w => w.AlbumId == albumId));

            IEnumerable<PhotoDto> photos = await _mockPhotoRepository.Object.GetPhotosAsync(albumId);

            Assert.DoesNotContain(photos, a => a.AlbumId != albumId);
        }
    }
}
