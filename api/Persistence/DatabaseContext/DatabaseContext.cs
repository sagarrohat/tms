using Domain;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Task;

namespace Persistence
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<PinnedTask> PinnedTasks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PinnedTask>(x =>
            {
                x.ToTable("pinned_tasks");

                x.HasKey(e 
                    => new
                    {
                        e.TaskId, e.UserId
                    });
                
                x.Property(e => e.TaskId).HasColumnName("task_id");
                x.Property(e => e.UserId).HasColumnName("user_id");
            });
            
            builder.Entity<Notification>(x =>
            {
                x.ToTable("notifications");

                x.HasKey(e => e.Id);
                
                x.Property(e => e.Id).HasColumnName("notification_id");
                x.Property(e => e.CreatedBy).HasColumnName("created_by");
                x.Property(e => e.CreatedOnUtc).HasColumnType("timestamp without time zone").HasColumnName("created_on_utc");
                x.Property(e => e.TaskId).HasColumnName("task_id");
                x.Property(e => e.Type).HasColumnName("type");
                x.Property(e => e.Unread).IsRequired().HasColumnName("unread").HasDefaultValueSql("true");
                x.Property(e => e.UserId).HasColumnName("user_id");
            });

            builder.Entity<Task>(x =>
            {
                x.ToTable("tasks");

                x.HasKey(e => e.Id);
                
                x.Property(e => e.Id).HasColumnName("task_id");
                x.Property(e => e.AssignedOnUtc).HasColumnType("timestamp without time zone").HasColumnName("assigned_on_utc");
                x.Property(e => e.AssignedUserId).HasColumnName("assigned_user_id");
                x.Property(e => e.CancelledOnUtc).HasColumnType("timestamp without time zone").HasColumnName("cancelled_on_utc");
                x.Property(e => e.CompletedOnUtc).HasColumnType("timestamp without time zone").HasColumnName("completed_on_utc");
                x.Property(e => e.CreatedBy).HasColumnName("created_by");
                x.Property(e => e.CreatedOnUtc).HasColumnType("timestamp without time zone").HasColumnName("created_on_utc");
                x.Property(e => e.Description).HasColumnName("description");
                x.Property(e => e.DueDate).HasColumnName("due_date_utc");
                x.Property(e => e.IsDeleted).HasColumnName("is_deleted");
                x.Property(e => e.ModifiedBy).HasColumnName("modified_by");
                x.Property(e => e.ModifiedOnUtc).HasColumnType("timestamp without time zone").HasColumnName("modified_on_utc");
                x.Property(e => e.PercentageCompleted).HasColumnName("percentage_completed");
                x.Property(e => e.Priority).HasColumnName("priority");
                x.Property(e => e.Status).HasColumnName("status");
                x.Property(e => e.Title).HasColumnName("title");
            });

            builder.Entity<User>(x =>
            {
                x.ToTable("users");
                
                x.HasKey(e => e.Id);
                
                x.HasIndex(e => e.EmailAddress, "users_email_address_key").IsUnique();
                
                x.Property(e => e.Id).HasColumnName("user_id");
                x.Property(e => e.EmailAddress).HasColumnName("email_address");
                x.Property(e => e.FirstName).HasColumnName("first_name");
                x.Property(e => e.IsActive).HasColumnName("is_active");
                x.Property(e => e.LastName).HasColumnName("last_name");
                x.Property(e => e.Pd).HasColumnName("pd");
            });
        }
    }
}
