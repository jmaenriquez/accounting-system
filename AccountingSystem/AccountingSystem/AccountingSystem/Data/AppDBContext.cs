using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace AccountingSystem.Data
{

    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options){

            Configuration = config ?? throw new ArgumentNullException(nameof(config));

        }

        // Define your tables here
        public DbSet<Account_List> Accounts { get; set; }
        public DbSet<journals> Journals { get; set; }
    }

}
