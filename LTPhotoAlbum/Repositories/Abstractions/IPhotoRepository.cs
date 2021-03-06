using System.Collections.Generic;
using System.Threading.Tasks;

namespace LTPhotoAlbum.Repositories.Abstractions
{
    public interface IPhotoRepository
    {

        public Task<IEnumerable<PhotoDto>> GetPhotosAsync(int? albumId);

    }
}
