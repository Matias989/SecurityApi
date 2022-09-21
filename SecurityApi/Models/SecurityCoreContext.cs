using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SecurityApi.Models
{
    public partial class SecurityCoreContext : DbContext
    {
        public SecurityCoreContext()
        {
        }

        public SecurityCoreContext(DbContextOptions<SecurityCoreContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<User>()
            .Property(u => u.IdUser)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
            .HasIndex(u => u.EmailUser)
            .IsUnique();
        }
        public DbSet<User> Users { get; set; }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
