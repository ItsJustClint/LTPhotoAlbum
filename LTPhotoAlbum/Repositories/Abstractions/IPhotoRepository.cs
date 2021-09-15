using System.Collections.Generic;
using System.Threading.Tasks;

namespace LTPhotoAlbum.Repositories.Abstractions
{
    public interface IPhotoRepository
    {
        public Task<IEnumerable<PhotoDto>> GetPhotosAsync(int? albumId);

        public Task<IEnumerable<int>> GetAlbumIdsAsync();

        public Task<IEnumerable<PhotoDto>> GetDataAsync(int? albumId);

    }
}
