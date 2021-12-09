using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Persistence;

namespace Kanban
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KanbanDbContext>
	{
		public KanbanDbContext CreateDbContext(string[] args)
		{
			var dbContextOptions = new DbContextOptionsBuilder<KanbanDbContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=mysecretpassword")
				.Options;
            return new KanbanDbContext(dbContextOptions, new ConfigurationManager());
		}
	}
}
