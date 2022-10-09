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

            var root = await Task.Run(() => JsonConvert.DeserializeObject<Rootobject>(content));

            var movie = new ShowDTO()
            {
                overview = root.results[0].overview,
                popularity = root.results[0].popularity,
                poster_path = root.results[0].poster_path,
                release_date = root.results[0].release_date,
                title = root.results[0].title,
                vote_average = root.results[0].vote_average,
                vote_count = root.results[0].vote_count
            };


            return movie;
        }

        public async Task<ShowDTO> GetSeries(string title, string key)
        {
            var response = await _httpClient.GetAsync($"search/movie?api_key={key}&query={title}");

            var content = await response.Content.ReadAsStringAsync();

            var root = await Task.Run(() => JsonConvert.DeserializeObject<Rootobject>(content));

            var series = new ShowDTO()
            {
                overview = root.results[0].overview,
                popularity = root.results[0].popularity,
                poster_path = root.results[0].poster_path,
                release_date = root.results[0].release_date,
                title = root.results[0].title,
                vote_average = root.results[0].vote_average,
                vote_count = root.results[0].vote_count
            };


            return series;
        }
    }
}
