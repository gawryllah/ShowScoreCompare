using Newtonsoft.Json;
using ShowScoreCompare.Data;
using ShowScoreCompare.Models;
using System.Web;
using static ShowScoreCompare.Data.ImdbIdGetter;

namespace ShowScoreCompare.Services
{
    public class ImdbService : IIMDB_Service
    {
        private readonly HttpClient _httpClient;

        public ImdbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ShowDTO> GetMovie(string title, string key)
        {
            ShowDTO show = null;

            var responseId = await _httpClient.GetAsync($"SearchMovie/{key}/{Uri.EscapeUriString(title)}");

            var idAnswer = await responseId.Content.ReadAsStringAsync();

            var idWrapper = await Task.Run(() => JsonConvert.DeserializeObject<ImdbModel>(idAnswer));

            if (idWrapper.results == null)
                return new ShowDTO();

            if (!(idWrapper?.results.Length < 1))
            {
                var response = await _httpClient.GetAsync($"Title/{key}/{idWrapper.results[0].id}/Ratings");
                var content = await response.Content.ReadAsStringAsync();

                var imdbResult = await Task.Run(() => JsonConvert.DeserializeObject<ImdbResult>(content));

                float raiting;
                int voteCounts;

                float.TryParse(imdbResult.imDbRating, out raiting);
                int.TryParse(imdbResult.imDbRatingVotes, out voteCounts);

                show = new ShowDTO()
                {
                    overview = imdbResult.plot,
                    popularity = 0f,
                    poster_path = imdbResult.image,
                    release_date = imdbResult.releaseDate,
                    title = imdbResult.title,
                    score = imdbResult.imDbRating,
                    vote_count = voteCounts
                };
            }


            return show;
        }

        public async Task<ShowDTO> GetSeries(string title, string key)
        {
            ShowDTO show = null;

            var responseId = await _httpClient.GetAsync($"SearchMovie/{key}/{Uri.EscapeUriString(title)}");

            var idAnswer = await responseId.Content.ReadAsStringAsync();

            var idWrapper = await Task.Run(() => JsonConvert.DeserializeObject<ImdbModel>(idAnswer));

            if (idWrapper.results == null)
                return new ShowDTO();

            if (!(idWrapper?.results.Length < 1))
            {
                var response = await _httpClient.GetAsync($"Title/{key}/{idWrapper.results[0].id}/Ratings");
                var content = await response.Content.ReadAsStringAsync();

                var imdbResult = await Task.Run(() => JsonConvert.DeserializeObject<ImdbResult>(content));

                float raiting;
                int voteCounts;

                float.TryParse(imdbResult.imDbRating, out raiting);
                int.TryParse(imdbResult.imDbRatingVotes, out voteCounts);

                show = new ShowDTO()
                {
                    overview = imdbResult.plot,
                    popularity = 0f,
                    poster_path = imdbResult.image,
                    release_date = imdbResult.releaseDate,
                    title = imdbResult.title,
                    score = imdbResult.imDbRating,
                    vote_count = voteCounts
                };
            }
            return show;
        }
    }
}
