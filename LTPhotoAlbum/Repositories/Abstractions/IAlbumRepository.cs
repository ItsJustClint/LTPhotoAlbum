using System.Collections.Generic;
using System.Threading.Tasks;

namespace LTPhotoAlbum.Repositories.Abstractions
{
    public interface IAlbumRepository
    {

        public Task<IEnumerable<int>> GetAlbumIdsAsync();

    }
}
