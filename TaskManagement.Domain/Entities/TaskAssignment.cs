using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Entities
{
    public class TaskAssignment
    {
        [Key, Column(Order = 0)]
        public int TaskItemId { get; set; }

        [Key, Column(Order = 1)]
        public int AppUserId { get; set; }

        [ForeignKey(nameof(TaskItemId))]
        public virtual TaskItem TaskItem { get; set; }

        [ForeignKey(nameof(AppUserId))]
        public virtual AppUser AppUser { get; set; }
    }
}
