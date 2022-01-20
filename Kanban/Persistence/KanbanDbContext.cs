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
            modelBuilder.Entity<Card>().HasKey(c => c.Id);
            modelBuilder.Entity<Card>().Property(c => c.Id).ValueGeneratedNever();
            modelBuilder.Entity<Card>().Property(c => c.Name).HasMaxLength(100);
            modelBuilder.Entity<Card>().Property(c => c.Description).HasMaxLength(250);
            modelBuilder.Entity<Card>().HasOne(c => c.Executor).WithMany();
            modelBuilder.Entity<Card>().Property(c => c.ColumnId);
            modelBuilder.Entity<Card>().HasMany(c => c.Comments).WithOne();
            modelBuilder.Entity<Card>().Property(c => c.CreationTime);
            
            modelBuilder.Entity<Card>().HasMany<Comment>("_comments").WithOne();
            modelBuilder.Entity<Card>().Navigation("_comments").AutoInclude();

            modelBuilder.Entity<Executor>().HasKey(e => e.Id);
            modelBuilder.Entity<Executor>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<Executor>().Property(e => e.AppUsername);

            modelBuilder.Entity<Column>().HasKey(c => c.Id);
            modelBuilder.Entity<Column>().Property(c => c.Id).ValueGeneratedNever();
            modelBuilder.Entity<Column>().Property(c => c.Name);
            modelBuilder.Entity<Column>().Property(c => c.OrderNumber);

            modelBuilder.Entity<Comment>().HasKey(c => c.Id);
            modelBuilder.Entity<Comment>().Property(c => c.Id).ValueGeneratedNever();
            modelBuilder.Entity<Comment>().HasOne(c => c.Author).WithMany();
            modelBuilder.Entity<Comment>().Property(c => c.Message).HasMaxLength(250);
            modelBuilder.Entity<Comment>().Property(c => c.CreationTime);

            modelBuilder.Entity<Board>().HasKey(b => b.Id);
            modelBuilder.Entity<Board>().Property(b => b.Id).ValueGeneratedNever();
            modelBuilder.Entity<Board>().Property(b => b.Name);
            
            modelBuilder.Entity<Board>().HasMany(b => b.Columns).WithOne();
            modelBuilder.Entity<Board>().Navigation(b => b.Columns).AutoInclude();
            
            modelBuilder.Entity<Board>().Ignore(b => b.Cards);
            modelBuilder.Entity<Board>().HasMany<Card>("_cards").WithOne();
            modelBuilder.Entity<Board>().Navigation("_cards").AutoInclude();

            modelBuilder.Entity<Chat>().HasKey(c => c.Id);
            modelBuilder.Entity<Chat>().Property(c => c.Id).ValueGeneratedNever();
            modelBuilder.Entity<Chat>().Property(c => c.App);
            modelBuilder.Entity<Chat>().Property(c => c.BoardId);
        }
    }
}