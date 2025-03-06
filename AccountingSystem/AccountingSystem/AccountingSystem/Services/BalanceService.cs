using AccountingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Services
{
    public class BalanceService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        public BalanceService (IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task <IEnumerable<Trial_Balance>> GetAllBalances()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.Balance.AsNoTracking().ToListAsync();
        }

    }
}
