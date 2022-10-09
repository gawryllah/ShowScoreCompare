using System.ComponentModel.DataAnnotations;

namespace ShowScoreCompare.Models
{
    public enum ShowType { Movie, Series }

    public class Show
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public int Views { get; set; }

        public ShowType Type { get; set; }


    }
}
