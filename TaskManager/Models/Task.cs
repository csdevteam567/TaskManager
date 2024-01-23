using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Task
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string? name { get; set; }

        [Required]
        public bool status { get; set; }

        public string? description { get; set; } = null;

        public ICollection<AssignedTask>? assignedTasks { get; set; } = null;

    }
}
