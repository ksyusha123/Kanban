using System.Collections.Generic;
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
            modelBuilder.Entity<Task>().HasKey(t => t.Id);
            modelBuilder.Entity<Task>().Property(t => t.Id).ValueGeneratedNever();
            modelBuilder.Entity<Task>().Property(t => t.Name).HasMaxLength(100);
            modelBuilder.Entity<Task>().Property(t => t.Description).HasMaxLength(250);
            modelBuilder.Entity<Task>().HasOne(t => t.Executor).WithMany();
            modelBuilder.Entity<Task>().HasOne(t => t.State).WithMany();
            modelBuilder.Entity<Task>().HasMany(t => t.Comments).WithOne();
            modelBuilder.Entity<Task>().Property(t => t.CreationTime);

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

            modelBuilder.Entity<Project>().HasKey(p => p.Id);
            modelBuilder.Entity<Project>().Property(p => p.Id).ValueGeneratedNever();
            modelBuilder.Entity<Project>().Property(p => p.Name).HasMaxLength(100);
            modelBuilder.Entity<Project>().Property(p => p.Description).HasMaxLength(250);
            modelBuilder.Entity<Project>().HasMany<Board>("_tables").WithOne();

            modelBuilder.Entity<Board>().HasKey(p => p.Id);
            modelBuilder.Entity<Board>().Property(p => p.Id).ValueGeneratedNever();
            modelBuilder.Entity<Board>().HasMany(t => t.Tasks);
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
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=mysecretpassword");
        }
    }
}