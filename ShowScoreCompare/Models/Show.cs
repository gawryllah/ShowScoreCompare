using System.ComponentModel.DataAnnotations;

namespace ShowScoreCompare.Models
{
    public class Show
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public int Views { get; set; }
    }
}
