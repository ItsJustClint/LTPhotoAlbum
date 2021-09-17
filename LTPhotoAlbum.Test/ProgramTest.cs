using AutoFixture;
using AutoFixture.AutoMoq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LTPhotoAlbum.Test
{
    public class ProgramTest
    {

        private readonly Random _random;
        private readonly IEnumerable<int> _albumIds;

        public ProgramTest()
        {
            Fixture fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());

            _random = new Random();

            _albumIds = fixture.CreateMany<int>();
        }

        [Fact]
        public void ValidAblumId()
        {
            int indexToUse = _random.Next(1, 3);

            int albumId = _albumIds.ElementAt(indexToUse);

            Assert.True(string.IsNullOrEmpty(Program.ValidateAlbumId(albumId.ToString(), _albumIds.ToList())));
        }

        [Fact]
        public void InvalidAlbumId_Numeric()
        {
            int albumId = _random.Next();

            while (_albumIds.Any(a => a == albumId))
            {
                albumId = _random.Next();
            }

            Assert.False(string.IsNullOrEmpty(Program.ValidateAlbumId(albumId.ToString(), _albumIds.ToList())));
        }

        [Fact]
        public void InvalidAlbumId_NonNumeric()
        {
            Assert.False(string.IsNullOrEmpty(Program.ValidateAlbumId("InvalidAlbumId", _albumIds.ToList())));
        }
    }
}
