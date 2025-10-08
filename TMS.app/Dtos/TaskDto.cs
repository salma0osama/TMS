using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.app.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
    }
    public class CreateTaskDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        [StringLength(200)]
        public string? Description { get; set; }
        //public bool IsCompleted { get; set; } = false;
    }
    public class UpdateTaskDto
    {
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        [StringLength(200)]
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }

    }
}
