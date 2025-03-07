using AccountingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Services
{
    public class JournalService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public JournalService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Journal_Entry>> GroupItems()
        {
            using var dbContext = _dbContextFactory.CreateDbContext(); // Create new instance
            
            
            return await dbContext.Journal.AsNoTracking()
                .Include(journal => journal.AccGrp)
                .ToListAsync();
        }
    }
}
