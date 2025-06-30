using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite PK for TaskAssignment
            modelBuilder.Entity<TaskAssignment>()
                .HasKey(ta => new { ta.TaskItemId, ta.AppUserId });

            modelBuilder.Entity<TaskAssignment>()
                .HasOne(ta => ta.TaskItem)
                .WithMany(t => t.Assignments)
                .HasForeignKey(ta => ta.TaskItemId);

            modelBuilder.Entity<TaskAssignment>()
                .HasOne(ta => ta.AppUser)
                .WithMany(u => u.TaskAssignments)
                .HasForeignKey(ta => ta.AppUserId);

        }




    }
}
