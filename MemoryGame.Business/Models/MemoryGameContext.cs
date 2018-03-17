using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MemoryGame.Business.Models
{
    public class MemoryGameContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<WordList> WordLists { get; set; }
        public DbSet<GameLevel> GameLevels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserState> UserState { get; set; }


        public MemoryGameContext(DbContextOptions<MemoryGameContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<GameLevel>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<User>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<UserState>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<WordList>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<WordList>()
                .Property(b => b.Randomize)
                .HasDefaultValue(false);

            modelBuilder.Entity<WordList>()
                .Property(b => b.Words)
                .IsRequired(true);


        }
    }
}
