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

        public async Task UpdateGrpList(IEnumerable<Group_List> groups)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            foreach (var group in groups)
            {
                var existingGroup = await dbContext.Groups.FirstOrDefaultAsync(x => x.id == group.id);

                if (existingGroup != null)
                {
                    existingGroup.name = group.name;
                    existingGroup.description = group.description;
                    existingGroup.type = group.type;
                    existingGroup.status = group.status;
                    existingGroup.isDeleted = group.isDeleted;

                    dbContext.Groups.Update(existingGroup); // Ensure EF tracks changes
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
