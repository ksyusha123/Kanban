using Domain;
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
            //todo доделать для остальных моделей
            
            modelBuilder.Entity<Task>().HasKey(t => t.Id);
            modelBuilder.Entity<Task>().Property(t => t.Name).HasMaxLength(100);
            modelBuilder.Entity<Task>().HasOne<IExecutor>().WithMany();
            modelBuilder.Entity<Task>().Property(t => t.Description).HasMaxLength(250);
            modelBuilder.Entity<Task>().HasOne<State>().WithMany();
        }
    }
}
