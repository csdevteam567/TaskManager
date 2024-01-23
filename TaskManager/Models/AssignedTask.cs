using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class AssignedTask
    {
        [Key]
        public int id { get; set; }

        public int EmployeeRole { get; set; }

        public int EmployeeId { get; set; }

        public int TaskId { get; set; }

        public Employee? employee { get; set; } = null;

        public Task? task { get; set; } = null;


    }
}
