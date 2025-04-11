using DBZ.Infraestructure.Models;
using Newtonsoft.Json;
namespace DBZ.Infraestructure.Services
{
    public class DragonBallApiService : IDragonBallApiService
    {
        private readonly HttpClient _httpClient;

        public DragonBallApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri("https://dragonball-api.com/api/");
        }

        public async Task<IEnumerable<Character>> GetCharactersAsync()
        {
            var response = await _httpClient.GetAsync("characters");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Character>>(content);
        }

        public async Task<IEnumerable<Transformation>> GetTransformationsAsync()
        {
            var response = await _httpClient.GetAsync("transformations");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Transformation>>(content);
        }
    }
}
