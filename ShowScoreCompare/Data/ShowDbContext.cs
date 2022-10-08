using Microsoft.EntityFrameworkCore;
using ShowScoreCompare.Models;

namespace ShowScoreCompare.Data
{
    public class ShowDbContext : DbContext
    {
        public ShowDbContext(DbContextOptions options) : base(options){}

        public DbSet<Show> ShowsDB { get; set; }
    }
}
