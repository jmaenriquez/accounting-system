using Microsoft.EntityFrameworkCore;
using AccountingSystem.Data;

namespace AccountingSystem.Services
{
    public class AccountService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public AccountService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Account_List>> GetAllAccounts()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.Accounts.AsNoTracking().ToListAsync();
        }
    }
}
