using GenealogyTree.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GenealogyTree.Infrastructure.Data
{
    public class GenealogyTreeContext : DbContext
    {
        public GenealogyTreeContext(DbContextOptions<GenealogyTreeContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<ParentChild> ParentsChildren { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Person>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Person>()
                .HasOne(x => x.User);

            modelBuilder.Entity<ParentChild>()
                .HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ParentChild>()
                .HasOne(x => x.Child)
                .WithMany(x => x.Parents)
                .HasForeignKey(u => u.ChildId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ParentChild>()
                .HasOne(x => x.CreatedByUser)
                .WithMany(x => x.CreatedRelations)
                .HasForeignKey(u => u.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Person>()
                .HasOne(x => x.CreatedByUser)
                .WithMany(x => x.CreatedPeople)
                .HasForeignKey(u => u.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
