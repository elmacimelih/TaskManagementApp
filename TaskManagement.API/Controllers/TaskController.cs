using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TaskItem>>> GetAll()
        {
            var tasks = await _service.GetAllAsync();
            return Ok(tasks);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskItem>> GetById(int id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskItem>> Create([FromBody] TaskItem task)
        {
            var created = await _service.CreateAsync(task);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskItem task)
        {
            var updated = await _service.UpdateAsync(id, task);
            if (!updated) return NotFound();
            return NoContent();
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // POST: api/tasks/{taskId}/assign/{userId}
        [HttpPost("{taskId:int}/assign/{userId:int}")]
        public async Task<IActionResult> Assign(int taskId, int userId)
        {
            var success = await _service.AssignUserAsync(taskId, userId);
            if (!success) return NotFound();

            return NoContent();
        }


        [HttpDelete("{taskId:int}/unassign/{userId}")]
        public async Task<IActionResult> UnAssign(int taskId, int userId)
        {
            var success = await _service.UnAssignUserAsync(taskId, userId);
            if (!success) return NotFound();

            return NoContent();
        }


        // GET: api/tasks/{taskId}/users
        [HttpGet("{taskId:int}/users")]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetAssignedUsers(int taskId)
        {
            var users = await _service.GetAssignedUserAsync(taskId);
            if (users == null || users.Count == 0) return NoContent();


            return Ok(users);
        }



    }
}
