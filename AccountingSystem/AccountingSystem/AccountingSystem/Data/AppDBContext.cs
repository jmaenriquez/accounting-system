using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AccountingSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Define your tables here
        public DbSet<Account_List> Accounts { get; set; }
        public DbSet<Group_List> Groups { get; set; }
        public DbSet<Journal_Entry> Journal { get; set; }
        

        // If necessary, configure options inside OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journal_Entry>()
                .Property(c => c.datetime)
                .HasColumnType("date"); // Ensure correct PostgreSQL storage
        }
    }
}
