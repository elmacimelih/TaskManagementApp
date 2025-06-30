using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces
{
    public interface ITaskAssignmentRepository
    {
        Task AddAsync(TaskAssignment taskAssignment);
        void Delete(TaskAssignment taskAssignment);
        Task<IReadOnlyList<TaskAssignment>> ListByTaskIdAsync(int taskId);
        Task<TaskAssignment?> GetByIdsAsync(int taskId, int userId);
        Task<int> SaveChangesAsync();

    }
}
