using Newtonsoft.Json;
using ShowScoreCompare.Data;

namespace ShowScoreCompare.Services
{
    public class TmDbService : ITMDB_Service
    {
        private readonly HttpClient _httpClient;

        public TmDbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ShowDTO> GetMovie(string title, string key)
        {
            ShowDTO show = null;

            var response = await _httpClient.GetAsync($"search/movie?api_key={key}&query={title}");

            var content = await response.Content.ReadAsStringAsync();

            var root = await Task.Run(() => JsonConvert.DeserializeObject<TmdbModel>(content));

            if (root.results == null || root.results[0].title == null)
                return new ShowDTO();

            if (!(root.results.Length < 1))
            {
                show = new ShowDTO()
                {
                    overview = root.results[0].overview,
                    popularity = root.results[0].popularity,
                    poster_path = $"https://image.tmdb.org/t/p/original{root.results[0].poster_path}",
                    release_date = root.results[0].release_date,
                    title = root.results[0].title,
                    vote_average = root.results[0].vote_average,
                    vote_count = root.results[0].vote_count
                };
            }


            return show;
        }

        public async Task<ShowDTO> GetSeries(string title, string key)
        {
            ShowDTO show = null;

            var response = await _httpClient.GetAsync($"search/tv?api_key={key}&query={title}");

            var content = await response.Content.ReadAsStringAsync();

            var root = await Task.Run(() => JsonConvert.DeserializeObject<TmdbModel>(content));

            if (root.results == null)
                return new ShowDTO();

            if (!(root.results.Length < 1))
            {
                show = new ShowDTO()
                {
                    overview = root.results[0].overview,
                    popularity = root.results[0].popularity,
                    poster_path = $"https://image.tmdb.org/t/p/original{root.results[0].poster_path}",
                    release_date = root.results[0].release_date,
                    title = root.results[0].title,
                    vote_average = root.results[0].vote_average,
                    vote_count = root.results[0].vote_count
                };
            }


            return show;
        }
    }
}
