using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerX.Database.Models;

namespace TaskTrackerX.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<TaskInfo> TaskInfos { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("taskTrackerX");
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new TaskInfoConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        private class RoleConfiguration : IEntityTypeConfiguration<Role>
        {
            public void Configure(EntityTypeBuilder<Role> builder)
            {
                builder
                    .HasKey(x => x.Id);

                builder
                    .Property(x => x.Name)
                    .IsRequired();
            }
        }

        private class TaskInfoConfiguration : IEntityTypeConfiguration<TaskInfo>
        {
            public void Configure(EntityTypeBuilder<TaskInfo> builder)
            {
                builder
                    .HasKey(x => x.Id);

                builder
                    .Property(x => x.Description)
                    .IsRequired();

                builder
                    .Property(x => x.Title)
                    .IsRequired();
                
                builder
                    .Property(x => x.Status)
                    .IsRequired();

                builder
                    .Property(x => x.DateReceived)
                    .IsRequired();

                builder
                    .Property(x => x.DateExpiration)
                    .IsRequired();
            }
        }

        private class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder
                    .HasKey(x => x.Id);

                builder
                    .Property(x => x.Name)
                    .IsRequired();

                builder
                    .HasMany(t => t.CreatedTask)
                    .WithOne()
                    .HasForeignKey(y => y.CreatedByUserId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder
                    .HasMany(t => t.AssignedTask)
                    .WithOne()
                    .HasForeignKey(y => y.AssignedToUserId)
                    .OnDelete(DeleteBehavior.SetNull);

                builder
                    .HasOne(x => x.Role)
                    .WithMany()
                    .HasForeignKey(y => y.RoleId)
                    .OnDelete(DeleteBehavior.NoAction);

                builder
                    .Property(x => x.Login)
                    .IsRequired();

                builder
                    .Property(x => x.Password)
                    .IsRequired();
            }
        }
    }
}
