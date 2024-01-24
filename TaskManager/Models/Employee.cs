using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Employee
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        public ICollection<Task>? Tasks { get; set; } = null;
    }
}
