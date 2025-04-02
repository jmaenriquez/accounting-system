using AccountingSystem.Components.Pages;
using AccountingSystem.Data;
using AccountingSystem.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AccountingSystem.Services
{
    public class JournalService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public JournalService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<IEnumerable<LedgerDto>> GetLedger()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.Journal
                .Include(j => j.AccName)
                .Where(j => !j.isDeleted)
                .GroupBy(j => j.AccNameId)
                .Select(entries => new LedgerDto
                {
                    AccountName = entries.First().AccName!.name,
                    Debit = entries.Sum(e => e.debit ?? 0),
                    Credit = entries.Sum(e => e.credit ?? 0)
                })
                
                .ToListAsync();
        }
        public async Task<IEnumerable<Journal_Entry>> journalentries()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            
            return await dbContext.Journal
                .AsNoTracking()
                .Include(j => j.AccGrp)
                .Include(j => j.AccName)
                .Where(x => !x.isDeleted)
                .OrderBy(j => j.TransactId)
                .ThenBy(j => j.debit > 0 ? 0 : 1)
                .ToListAsync();
        }

        public async Task<IEnumerable<Journal_Entry>> archive()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Journal
                .AsNoTracking()
                .Include(j => j.AccGrp)
                .Include(j => j.AccName)
                .Where(x => x.isDeleted)
                .OrderBy(j => j.TransactId)
                .ThenBy(j => j.debit > 0 ? 0 : 1)
                .ToListAsync();
        }

        public async Task UpdateJournalEntries(IEnumerable<Journal_Entry> entries)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            foreach (var entry in entries)
            {
                var existingData = await dbContext.Journal
                    .Where(e => e.TransactId == entry.TransactId)
                    .ToListAsync();
                if (existingData.Any())
                {
                    foreach (var existingEntry in existingData)
                    {
                        existingEntry.isDeleted = entry.isDeleted;
                    }
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
