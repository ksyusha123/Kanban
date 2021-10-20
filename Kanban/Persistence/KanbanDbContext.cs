using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class KanbanDbContext : DbContext
    {
        public KanbanDbContext(DbContextOptions<KanbanDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //todo добавить описание моделей, когда они появятся
        }
    }
}
