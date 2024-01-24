using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Task
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string? Name {  get; set; }

        [Required]
        public bool CompletionStatus { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime Deadline { get; set; }

        public string? Description { get; set; } = null;

        public ICollection<AssignedTask>? assignedTasks { get; set; } = null;

    }
}
