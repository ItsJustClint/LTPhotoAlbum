using AutoFixture;
using AutoFixture.AutoMoq;
using LTPhotoAlbum.Repositories;
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
        private readonly Mock<IPhotoAlbumDataRepository> _mockRepo;
        private readonly PhotoRepository _photoRepository;
        private readonly IEnumerable<PhotoDto> _serviceRecords;

        public PhotoRepositoryTest()
        {
            Fixture fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());

            _random = new Random();

            _serviceRecords = fixture.CreateMany<PhotoDto>();

            _mockRepo = new Mock<IPhotoAlbumDataRepository>();
            _photoRepository = new PhotoRepository(_mockRepo.Object);
        }

        [Fact]
        public async void PhotosReturnedAsync()
        {
            _mockRepo.Setup(s => s.GetDataAsync(null)).ReturnsAsync(_serviceRecords);

            var photos = await _photoRepository.GetPhotosAsync(null);

            Assert.NotEmpty(photos);
        }

        [Fact]
        public async void OutputMatchesExpectedAsync()
        {
            _mockRepo.Setup(s => s.GetDataAsync(null)).ReturnsAsync(_serviceRecords);

            var photos = await _photoRepository.GetPhotosAsync(null);

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

            _mockRepo.Setup(s => s.GetDataAsync(albumId)).ReturnsAsync(_serviceRecords.Where(w => w.AlbumId == albumId));

            var photos = await _photoRepository.GetPhotosAsync(albumId);

            Assert.Empty(photos);
        }

        [Fact]
        public async void NoNonFilteredPhotosReturnedAsync()
        {
            int indexToUse = _random.Next(1, 3);

            int albumId = _serviceRecords.ElementAt(indexToUse).AlbumId;

            _mockRepo.Setup(s => s.GetDataAsync(albumId)).ReturnsAsync(_serviceRecords.Where(w => w.AlbumId == albumId));

            var photos = await _photoRepository.GetPhotosAsync(albumId);

            Assert.DoesNotContain(photos, a => a.AlbumId != albumId);
        }
    }
}
