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

        //DTO for Dashboard
        public async Task<IEnumerable<StatementDTO>> Dashboard()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.Journal
                .Include(j => j.AccName)
                .Include(j => j.AccGrp)
                .Where(j => !j.isDeleted)
                .Where(j => j.AccGrp!.name == "Asset")
                .Where(j => j.AccName!.name == "Capital" || j.AccName!.name == "Payables" ||
                        j.AccName!.name == "Receivables")
                .GroupBy(j => j.AccNameId)
                .Select(entries => new StatementDTO
                {
                    AccountName = entries.First().AccName!.name,
                    AccountGroup = entries.First().AccGrp!.name,
                    Debit = entries.Sum(e => e.debit ?? 0),
                    Credit = entries.Sum(e => e.credit ?? 0)
                })
                .ToListAsync();
        }

        //DTO for Trial Balance Page
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
                    Credit = entries.Sum(e => e.credit ?? 0),
                    SumOfBalance = entries.Sum(e => e.debit ?? 0) - entries.Sum(e => e.credit ?? 0)
                })
                
                .ToListAsync();
        }

        //DTO for Income Statement
        public async Task <IEnumerable <StatementDTO>> GetStatement()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.Journal
                .Include(j => j.AccName)
                .Include(j => j.AccGrp)
                .Where(j => !j.isDeleted)
                .Where(j => j.AccGrp!.name == "Expense" || j.AccGrp.name == "Revenue")
                .GroupBy(j => j.AccNameId)
                .Select(entries => new StatementDTO
                {
                    AccountName = entries.First().AccName!.name,
                    AccountGroup = entries.First().AccGrp!.name,
                    Debit = entries.Sum(e => e.debit ?? 0),
                    Credit = entries.Sum(e => e.credit ?? 0)
                })
                .ToListAsync();
        }

        //Balance Sheet
        public async Task<IEnumerable<StatementDTO>> BalanceSheet()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var groups = await dbContext.Journal
                .Include(j => j.AccName)
                .Include(j => j.AccGrp)
                .Where(j => !j.isDeleted)
                .Where(j => j.AccGrp!.name == "Asset" || j.AccGrp.name == "Liability" ||
                            j.AccGrp.name == "Equity" || j.AccGrp.name == "Revenue" || j.AccGrp.name == "Expense")
                .GroupBy(j => j.AccNameId)
                .Select(entries => new StatementDTO
                {
                    AccountName = entries.First().AccName!.name,
                    AccountGroup = entries.First().AccGrp!.name,
                    Debit = entries.Sum(e => e.debit ?? 0),
                    Credit = entries.Sum(e => e.credit ?? 0)
                })
                .ToListAsync();

            foreach (var item in groups)
            {
                switch (item.AccountGroup)
                {
                    case "Asset":
                    case "Expense":
                        item.SumOfBalance = item.Debit - item.Credit;
                        break;
                    case "Liability":
                    case "Equity":
                    case "Revenue":
                        item.SumOfBalance = item.Credit - item.Debit;
                        break;
                    default:
                        item.SumOfBalance = 0;
                        break;
                }
            }

            return groups;
        }


        //Fetch data from Journal Table
        public async Task<IEnumerable<Journal_Entry>> journalentries()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            
            return await dbContext.Journal
                .AsNoTracking()
                .Include(j => j.AccGrp)
                .Include(j => j.AccName)
                .Where(x => !x.isDeleted)
                .OrderBy(j => j.datetime)
                .ThenBy(j => j.debit > 0 ? 0 : 1)
                .ToListAsync();
        }


        //Send all "deleted" data to Archives
        public async Task<IEnumerable<Journal_Entry>> archive()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Journal
                .AsNoTracking()
                .Include(j => j.AccGrp)
                .Include(j => j.AccName)
                .Where(x => x.isDeleted)
                .OrderBy(j => j.datetime)
                .ThenBy(j => j.debit > 0 ? 0 : 1)
                .ToListAsync();
        }

        //Update database if data is soft deleted
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
