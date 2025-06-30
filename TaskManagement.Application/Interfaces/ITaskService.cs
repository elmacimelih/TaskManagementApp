using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IReadOnlyList<TaskItem>> GetAllAsync();
        Task<TaskItem> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<bool> UpdateAsync(int id, TaskItem task);
        Task<bool> DeleteAsync(int id);

        Task<bool> AssignUserAsync(int taskId, int userId);
        Task<bool> UnAssignUserAsync(int taskId, int userId);
        Task<IReadOnlyList<AppUser>> GetAssignedUserAsync(int taskId);




    }
}
