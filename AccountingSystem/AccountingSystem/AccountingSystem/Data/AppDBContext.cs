using AccountingSystem.Components.Pages;
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
        public DbSet<Group_List> Groups { get; set; }
        public DbSet<Credited_Accounts> Credit { get; set; }
        public DbSet<Debit_Accounts> Debit { get; set; }

        public DbSet<Amount> Amounts { get; set; }

        public DbSet<Journal_Entry> Journal { get; set; }
        
        public DbSet<Trial_Balance> Balance { get; set; }

        //public DbSet<Bank_Records> BankRecords { get; set; }
    }

}
