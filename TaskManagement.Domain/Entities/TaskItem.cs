using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.Entities
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public Common.TaskStatus Status { get; set; }

        [Required]
        public Common.TaskPriority Priority { get; set; }

        // Many-to-many ilişki
        public virtual ICollection<TaskAssignment> Assignments { get; set; } = new List<TaskAssignment>();

    }
}
