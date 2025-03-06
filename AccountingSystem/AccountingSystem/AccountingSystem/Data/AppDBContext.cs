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
        public DbSet<Credited_Accounts> Credit { get; set; }
        public DbSet<Debit_Accounts> Debit { get; set; }
        public DbSet<Amount> Amounts { get; set; }
        public DbSet<Journal_Entry> Journal { get; set; }
        public DbSet<Trial_Balance> Balance { get; set; }

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
    }
}
