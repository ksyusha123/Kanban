using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence;

namespace Kanban
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KanbanDbContext>
	{
		public KanbanDbContext CreateDbContext(string[] args)
		{
			var dbContextOptions = new DbContextOptionsBuilder<KanbanDbContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345")
				.Options;
            return new KanbanDbContext(dbContextOptions);
		}
	}
}
