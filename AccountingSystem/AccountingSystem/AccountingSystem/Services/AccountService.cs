using Microsoft.EntityFrameworkCore;
using AccountingSystem.Data;
using MudBlazor.Interfaces;

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
            return await dbContext.Accounts
                .AsNoTracking()
                .Where(x => !x.isDeleted)
                .OrderBy(a => a.id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Account_List>> AccArchives()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.Accounts
                .AsNoTracking()
                .Where(x => x.isDeleted)
                .OrderBy(a => a.id)
                .ToListAsync();
        }

        public async Task UpdateAccount(IEnumerable<Account_List> accounts)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            foreach (var account in accounts)
            {
                var existingAccount = await dbContext.Accounts.FindAsync(account.id);
                if (existingAccount != null)
                {
                    existingAccount.name = account.name;
                    existingAccount.description = account.description;
                    existingAccount.status = account.status;
                    existingAccount.isDeleted = account.isDeleted;
                    dbContext.Accounts.Update(existingAccount);
                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
