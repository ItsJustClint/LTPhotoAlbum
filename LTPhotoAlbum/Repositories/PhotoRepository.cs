using LTPhotoAlbum.Repositories.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LTPhotoAlbum.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {

        private readonly IPhotoAlbumDataRepository _photoAlbumDataRepository;

        public PhotoRepository(IPhotoAlbumDataRepository photoAlbumDataRepository)
        {
            _photoAlbumDataRepository = photoAlbumDataRepository;
        }

        public async Task<IEnumerable<PhotoDto>> GetPhotosAsync(int? albumId)
        {
            return await _photoAlbumDataRepository.GetDataAsync(albumId);
        }
    }
}
