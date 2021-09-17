using LTPhotoAlbum.Repositories.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTPhotoAlbum.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {

        private readonly IPhotoAlbumDataRepository _photoAlbumDataRepository;

        public AlbumRepository(IPhotoAlbumDataRepository photoAlbumDataRepository)
        {
            _photoAlbumDataRepository = photoAlbumDataRepository;
        }

        public async Task<IEnumerable<int>> GetAlbumIdsAsync()
        {
            return ((List<PhotoDto>)await _photoAlbumDataRepository.GetDataAsync(null)).Select(s => s.AlbumId).Distinct().ToList();
        }
    }
}
