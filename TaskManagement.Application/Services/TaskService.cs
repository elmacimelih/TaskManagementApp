using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Persistence;  // bunu kullanabilmek için Application → Infrastructure referansı ekle

namespace TaskManagement.Application.Services
{
    public class TaskService : ITaskService //Inheritance miras alma
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IAppUserRepository _userRepo;
        private readonly ITaskAssignmentRepository _assignmentRepo;

        public TaskService(ITaskRepository taskRepo, IAppUserRepository userRepo, ITaskAssignmentRepository assignmentRepo)
        {
            _taskRepo = taskRepo;
            _userRepo = userRepo;
            _assignmentRepo = assignmentRepo;
        }

        public async Task<IReadOnlyList<TaskItem>> GetAllAsync()
        {
            return await _taskRepo.ListAllAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _taskRepo.GetByIdAsync(id);
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            task.CreatedAt = DateTime.UtcNow;
            await _taskRepo.AddAsync(task);
            await _taskRepo.SaveChangesAsync();
            return task;
        }

        public async Task<bool> UpdateAsync(int id, TaskItem task)
        {
            var existing = await _taskRepo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.DueDate = task.DueDate;
            existing.Status = task.Status;
            existing.Priority = task.Priority;

            _taskRepo.Update(existing);
            await _taskRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _taskRepo.GetByIdAsync(id);
            if (existing == null) return false;

            _taskRepo.Delete(existing);
            await _taskRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignUserAsync(int taskId, int userId)
        {
            // 1. Görev ve kullanıcı var mı?
            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task == null) return false;

            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return false;

            // 2. Zaten atanmış mı?
            var exists = await _assignmentRepo.GetByIdsAsync(taskId, userId);
            if (exists != null) return false;

            // 3. Atamayı ekle
            var assignmet = new TaskAssignment();
            assignmet.TaskItemId = taskId;
            assignmet.AppUserId = userId;

            await _assignmentRepo.AddAsync(assignmet);
            await _assignmentRepo.SaveChangesAsync();

            return true;

        }

        public async Task<bool> UnAssignUserAsync(int taskId, int userId)
        {
            // 1. Atama var mı?
            var assignment = await _assignmentRepo.GetByIdsAsync(taskId, userId);
            if (assignment == null) return false;

            // 2. Atamayı sil
            _assignmentRepo.Delete(assignment);
            await _assignmentRepo.SaveChangesAsync();

            return true;

        }

        public async Task<IReadOnlyList<AppUser>> GetAssignedUserAsync(int taskId)
        {
            var assignments = await _assignmentRepo.ListByTaskIdAsync(taskId);

            return assignments
                .Select(ta => ta.AppUser)
                .ToList();

        }


    }
}
