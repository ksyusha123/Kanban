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
                .UseNpgsql("Host=ec2-54-228-95-1.eu-west-1.compute.amazonaws.com;Port=5432;Database=d7t125uijct5dg;Username=ytplbqznoicmdy;Password=a198a62df54df84a919e1c4610446ae978acb5d43671783f61efd4e8c52ca492;sslmode=Require;Trust Server Certificate=true;")
				.Options;
            return new KanbanDbContext(dbContextOptions);
		}
	}
}
