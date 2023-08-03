using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerX.TaskApi.Models;

namespace TaskTrackerX.TaskApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExerciseConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        private class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
        {
            public void Configure(EntityTypeBuilder<Exercise> builder)
            {
                builder
                    .HasKey(x => x.Id);

                builder
                    .Property(x => x.Subject)
                    .IsRequired();

                builder
                    .Property(x => x.Description)
                    .IsRequired();

                builder
                    .HasOne(x => x.ExerciseStatus)
                    .WithMany()
                    .HasForeignKey(x => x.ExerciseStatusId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                builder
                    .HasIndex(x => x.ExerciseStatusId);

                builder
                    .Property(x => x.CreatedByUserId)
                    .IsRequired();

                builder
                    .HasIndex(x => x.CreatedByUserId);

                builder
                    .Property(x => x.AssignedToUserId)
                    .IsRequired();
                
                builder
                    .HasIndex(x => x.AssignedToUserId);

                builder
                    .Property(x => x.ReceivedDate)
                    .IsRequired();

                builder
                    .Property(x => x.Deadline)
                    .IsRequired();
            }
        }

        private class StatusConfiguration : IEntityTypeConfiguration<Status>
        {
            public void Configure(EntityTypeBuilder<Status> builder)
            {
                builder
                    .HasKey(x => x.Id);

                builder
                    .Property(x => x.Name)
                    .IsRequired();

                builder
                    .HasIndex(x => x.Name)
                    .IsUnique();
            }
        }
    }
}
