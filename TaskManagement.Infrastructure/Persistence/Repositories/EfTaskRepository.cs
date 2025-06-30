using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Infrastructure.Persistence.Repositories
{
    public class EfTaskRepository : ITaskRepository
    {
        private readonly AppDbContext _db;

        public EfTaskRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(TaskItem entity)
        {
            await _db.Tasks.AddAsync(entity);
        }

        public void Delete(TaskItem entity)
        {
            _db.Tasks.Remove(entity);
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _db.Tasks.FindAsync(id);
        }

        public async Task<IReadOnlyList<TaskItem>> ListAllAsync()
        {
            return await _db.Tasks
                .Include(t => t.Assignments)
                .ThenInclude(a => a.AppUser)
                .ToListAsync();
        }

        public void Update(TaskItem entity)
        {
            _db.Tasks.Update(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }



    }
}
