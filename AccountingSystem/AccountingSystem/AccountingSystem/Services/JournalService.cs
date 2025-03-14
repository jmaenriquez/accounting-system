using AccountingSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AccountingSystem.Services
{
    public class JournalService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public JournalService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Journal_Entry>> journalentries()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Journal
                .AsNoTracking()
                .Include(j => j.AccGrp)
                .Include(j => j.AccName)
                .OrderBy(j => j.TransactId)
                .ThenBy(j => j.debit > 0 ? 0 : 1)
                .Select(j => new Journal_Entry
                {
                    TransactId = j.TransactId,
                    datetime = j.datetime.Date,
                    AccName = j.AccName,
                    description = j.description,
                    debit = j.debit,
                    credit = j.credit
                })
                .Where(x => !x.isDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Journal_Entry>> archive()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Journal
                .AsNoTracking()
                .Include(j => j.AccGrp)
                .Include(j => j.AccName)
                .OrderBy(j => j.TransactId)
                .ThenBy(j => j.debit > 0 ? 0 : 1)
                .Select(j => new Journal_Entry
                {
                    TransactId = j.TransactId,
                    datetime = j.datetime.Date,
                    AccName = j.AccName,
                    description = j.description,
                    debit = j.debit,
                    credit = j.credit
                })
                .Where(x => x.isDeleted)
                .ToListAsync();
        }

        public async Task UpdateJournalEntries(IEnumerable<Journal_Entry> entries)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            foreach (var entry in entries)
            {
                dbContext.Journal.Update(entry);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
