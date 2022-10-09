using Newtonsoft.Json;
using ShowScoreCompare.Data;

namespace ShowScoreCompare.Services
{
    public class MovieDbService : IMovieDbService
    {
        private readonly HttpClient _httpClient;

        public MovieDbService(HttpClient httpClient)        {
            _httpClient = httpClient;
        }

        public async Task<ShowDTO> GetMovie(string title, string key)
        {
            var response = await _httpClient.GetAsync($"search/movie?api_key={key}&query={title}");

            var content = await response.Content.ReadAsStringAsync();

            ShowDTO? movie = await Task.Run(() => JsonConvert.DeserializeObject<ShowDTO>(content));

            return movie;


        }

        public async Task<ShowDTO> GetSeries(string title, string key)
        {
            var response = await _httpClient.GetAsync($"search/tv?api_key={key}&query={title}");

            var content = await response.Content.ReadAsStringAsync();

            var series = await Task.Run(() => JsonConvert.DeserializeObject<List<ShowDTO>>(content));

            return series[0];
        }
    }
}
