using ShowScoreCompare.Data;

namespace ShowScoreCompare.Services
{
    public interface IMovieDbService
    {
        Task<ShowDTO> GetMovie(string title, string key);
        Task<ShowDTO> GetSeries(string title, string key);
    }
}
