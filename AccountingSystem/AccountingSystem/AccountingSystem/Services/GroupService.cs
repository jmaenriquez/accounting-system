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
            return await dbContext.Groups.AsNoTracking()
                .Where(x => !x.isDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Group_List>> archive()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.Groups.AsNoTracking()
                .Where(x => x.isDeleted)
                .ToListAsync();
        }

        public async Task UpdateGrpList(IEnumerable <Group_List> groups)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            foreach (var group in groups)
            {
                var existingGroup = await dbContext.Groups
                    .Where(x => x.id == group.id)
                    .ToListAsync();

                if (existingGroup.Any())
                {
                    foreach(var data in existingGroup)
                    {
                        data.isDeleted = group.isDeleted;
                    }
                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
