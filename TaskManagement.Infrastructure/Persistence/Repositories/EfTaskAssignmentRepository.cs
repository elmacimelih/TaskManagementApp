using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Infrastructure.Persistence.Repositories
{
    public class EfTaskAssignmentRepository : ITaskAssignmentRepository
    {
        private readonly AppDbContext _db;

        public EfTaskAssignmentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(TaskAssignment taskAssignment)
        {
            await _db.TaskAssignments.AddAsync(taskAssignment);
        }

        public void Delete(TaskAssignment taskAssignment)
        {
            _db.TaskAssignments.Remove(taskAssignment);
        }


        public async Task<TaskAssignment?> GetByIdsAsync(int taskId, int userId)
        {
            return await _db.TaskAssignments.FindAsync(taskId, userId);
        }

        public async Task<IReadOnlyList<TaskAssignment>> ListByTaskIdAsync(int taskId)
        {
            return await _db.TaskAssignments
                .Include(ta => ta.AppUser)
                .Where(ta => ta.TaskItemId == taskId)
                .ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }


    }
}
