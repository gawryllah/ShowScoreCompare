using ShowScoreCompare.Data;

namespace ShowScoreCompare.Services
{
    public interface IIMDB_Service
    {
        Task<ShowDTO> GetMovie(string title, string key);
        Task<ShowDTO> GetSeries(string title, string key);
    }
}
