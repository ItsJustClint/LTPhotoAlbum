using LTPhotoAlbum.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LTPhotoAlbum.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {

        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public IEnumerable<PhotoDto> photos { get; set; }

        public PhotoRepository(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<PhotoDto>> GetPhotosAsync(int? albumId)
        {
            return await GetDataAsync(albumId);
        }

        public async Task<IEnumerable<int>> GetAlbumIdsAsync()
        {
            return ((List<PhotoDto>)await GetDataAsync(null)).Select(s => s.AlbumId).Distinct().ToList();
        }

        public async Task<IEnumerable<PhotoDto>> GetDataAsync(int? albumId)
        {
            List<PhotoDto> photos = new List<PhotoDto>();

            string photoUrl = _configuration.GetValue<string>("PhotoAlbumBaseURL", null);

            if (!(albumId is null)) photoUrl += $"{ _configuration.GetValue<string>("AlbumQueryString", null)}{albumId}";

            var request = new HttpRequestMessage(HttpMethod.Get, photoUrl);
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            string responseJson = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                using (var reader = new StreamReader(responseStream))
                {
                    responseJson = reader.ReadToEnd();
                }
            }

            if (!string.IsNullOrWhiteSpace(responseJson))
            {
                var jsonOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                photos = (List<PhotoDto>)JsonSerializer.Deserialize(responseJson, typeof(List<PhotoDto>), jsonOptions);
            }

            return photos;
        }
    }
}
