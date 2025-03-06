using Microsoft.EntityFrameworkCore;
using AccountingSystem.Data;

namespace AccountingSystem.Services
{
    public class GroupService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public GroupService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Group_List>> GroupItems()
        {
            using var dbContext = _dbContextFactory.CreateDbContext(); // Create new instance
            return await dbContext.Groups.AsNoTracking().ToListAsync();
        }
    }
}
