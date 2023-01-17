using GenealogyTree.Domain.Enums;
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
        public DbSet<Marriage> Spouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Person>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Person>()
                .HasOne(x => x.User);

            modelBuilder.Entity<Person>().Property(x => x.Sex)
                .HasConversion(v => v.ToString(), v => string.IsNullOrEmpty(v) ? ESex.Other : (ESex)Enum.Parse(typeof(ESex), v))
                .IsRequired();

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

            modelBuilder.Entity<Marriage>()
                .HasOne(x => x.Person)
                .WithMany(x => x.Spouses)
                .HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Marriage>()
                .HasOne(x => x.SpousePerson)
                .WithMany(x => x.Marriages)
                .HasForeignKey(u => u.SpouseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Marriage>()
                .HasOne(x => x.CreatedByUser)
                .WithMany(x => x.AddedMarriages)
                .HasForeignKey(u => u.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
