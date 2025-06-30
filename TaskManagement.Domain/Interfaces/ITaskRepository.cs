using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<IReadOnlyList<TaskItem>> ListAllAsync();
        Task<TaskItem> GetByIdAsync(int id);
        Task AddAsync(TaskItem entity);
        void Update(TaskItem entity);
        void Delete(TaskItem entity);
        Task<int> SaveChangesAsync();  // EF Core’un SaveChangesAsync’ini soyutluyoruz
    }
}
