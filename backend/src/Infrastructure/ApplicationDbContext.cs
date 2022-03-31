using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CardGroup> CardGroups { get; set; }
        public DbSet<Card> Cards { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CardGroup>().Property(cg => cg.CardGroupId).IsRequired().ValueGeneratedNever();

            modelBuilder.Entity<CardGroup>().HasMany<Card>(cg => cg.Cards).WithOne(c => c.BelongsTo).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Card>().Property(c => c.CardId).IsRequired().ValueGeneratedNever();

            modelBuilder.Entity<Activity>().Property(a => a.ActivityId).IsRequired().ValueGeneratedNever();

            modelBuilder.Entity<User>().HasIndex(u => u.Username);

            modelBuilder.Entity<Board>().OwnsOne(b => b.BgColor);

            modelBuilder.Entity<Board>().HasMany<Activity>(b => b.Activities).WithOne(a => a.BelongsTo).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Board>().HasMany<Invitation>().WithOne(o => o.InvitedTo).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Board>().HasMany<CardGroup>(b => b.CardGroups).WithOne().OnDelete(DeleteBehavior.Cascade);




            modelBuilder.Entity<Card>().OwnsOne(c => c.Color);
            base.OnModelCreating(modelBuilder);
        }
    }
}