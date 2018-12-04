using Microsoft.EntityFrameworkCore;

namespace PS.MAD.D4AS.DataAccess
{
    public class TicketingContext : DbContext
    {
        public DbSet<Model.Ticket> Tickets { get; set; }
        public DbSet<Model.Image> Images { get; set; }
        public DbSet<Model.Video> Videos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:ps-mad-d4as.database.windows.net,1433;Initial Catalog=Tickets;Persist Security Info=False;User ID=nikolamilanovic;Password=Css658923;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Model.Ticket>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Model.Image>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Model.Video>().Property(v => v.Id).ValueGeneratedOnAdd();
        }
    }
}
