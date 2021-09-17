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
    public class AlbumRepositoryTest
    {
        private readonly Random _random;
        private readonly Mock<IAlbumRepository> _mockAlbumRepository;
        private readonly IEnumerable<int> _serviceRecords;

        public AlbumRepositoryTest()
        {
            Fixture fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());

            _random = new Random();

            _serviceRecords = fixture.CreateMany<int>();

            _mockAlbumRepository = new Mock<IAlbumRepository>();
        }

        [Fact]
        public async void AlbumsIdsReturnedAsync()
        {
            _mockAlbumRepository.Setup(s => s.GetAlbumIdsAsync()).ReturnsAsync(_serviceRecords);

            IEnumerable<int> albumIds = await _mockAlbumRepository.Object.GetAlbumIdsAsync();

            Assert.NotEmpty(albumIds);
        }

        [Fact]
        public async void OutputMatchesExpectedAsync()
        {
            _mockAlbumRepository.Setup(s => s.GetAlbumIdsAsync()).ReturnsAsync(_serviceRecords.Distinct().ToList());

            IEnumerable<int> albums = await _mockAlbumRepository.Object.GetAlbumIdsAsync();

            Assert.Equal(_serviceRecords.Distinct().ToList(), albums);
        }
    }
}
