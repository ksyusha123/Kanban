using System;

namespace Kanban
{
    class Program
    {
        static void Main(string[] args)
        {
            var designTimeDbContextFactory = new DesignTimeDbContextFactory();
            var kanbanDbContext = designTimeDbContextFactory.CreateDbContext(Array.Empty<string>());
        }
    }
}
