using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public class KanbanDbContext : DbContext
    {
        public KanbanDbContext(DbContextOptions<KanbanDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().HasKey(t => t.Id);
            modelBuilder.Entity<Card>().Property(t => t.Id).ValueGeneratedNever();
            modelBuilder.Entity<Card>().Property(t => t.Name).HasMaxLength(100);
            modelBuilder.Entity<Card>().Property(t => t.Description).HasMaxLength(250);
            modelBuilder.Entity<Card>().HasOne(t => t.Executor).WithMany();
            modelBuilder.Entity<Card>().HasOne(t => t.State).WithMany();
            modelBuilder.Entity<Card>().HasMany(t => t.Comments).WithOne();
            modelBuilder.Entity<Card>().Property(t => t.CreationTime);

            modelBuilder.Entity<Executor>().HasKey(e => e.Id);
            modelBuilder.Entity<Executor>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<Executor>().Property(e => e.Name).HasMaxLength(100);
            modelBuilder.Entity<Executor>().Property(e => e.TelegramUsername).HasMaxLength(100);

            modelBuilder.Entity<State>().HasKey(s => s.Id);
            modelBuilder.Entity<State>().Property(s => s.Id).ValueGeneratedNever();
            modelBuilder.Entity<State>().Property(s => s.Name);
            modelBuilder.Entity<State>().HasMany(s => s.NextStates).WithMany(s => s.PrevStates);

            modelBuilder.Entity<Comment>().HasKey(c => c.Id);
            modelBuilder.Entity<Comment>().Property(c => c.Id).ValueGeneratedNever();
            modelBuilder.Entity<Comment>().HasOne(c => c.Author).WithMany();
            modelBuilder.Entity<Comment>().Property(c => c.Message).HasMaxLength(250);
            modelBuilder.Entity<Comment>().Property(c => c.CreationTime);

            modelBuilder.Entity<Board>().HasKey(p => p.Id);
            modelBuilder.Entity<Board>().Property(p => p.Id).ValueGeneratedNever();
            modelBuilder.Entity<Board>().Property(p => p.Name);
            modelBuilder.Entity<Board>().HasMany(t => t.Cards);
            modelBuilder.Entity<Board>().HasMany(t => t.States);
            modelBuilder.Entity<Board>().OwnsMany<ExecutorsWithRights>("ExecutorsWithRights", er =>
            {
                er.HasKey(e => e.Id);
                er.Property(e => e.Id).ValueGeneratedNever();
                er.Property(e => e.ExecutorId);
                er.Property(e => e.Rights);
            });

            modelBuilder.Entity<Chat>().HasKey(c => c.Id);
            modelBuilder.Entity<Chat>().Property(c => c.Id).ValueGeneratedNever();
            modelBuilder.Entity<Chat>().Property(c => c.App);
            modelBuilder.Entity<Chat>().Property(c => c.ProjectId);
        }
    }
}