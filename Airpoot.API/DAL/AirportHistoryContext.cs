using Microsoft.EntityFrameworkCore;

namespace Airpoot.API.DAL
{
    public class AirportHistoryContext : DbContext
    {
        public AirportHistoryContext(DbContextOptions<AirportHistoryContext> options) : base(options) { }
        public DbSet<AirplaneHistory> AirplanesHistory { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AirplaneHistory>()
        //}
    }
}
