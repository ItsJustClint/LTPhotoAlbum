using System.Collections.Generic;
using System.Threading.Tasks;

namespace LTPhotoAlbum.Repositories.Abstractions
{
    public interface IPhotoAlbumDataRepository
    {

        public Task<IEnumerable<PhotoDto>> GetDataAsync(int? albumId);

    }
}
