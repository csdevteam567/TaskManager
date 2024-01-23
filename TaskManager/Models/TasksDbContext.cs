using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> dbContextOptions)
            :base(dbContextOptions)
        {
        }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<AssignedTask> AssignedTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP\SQLEXPRESS;Initial Catalog=TasksDb;User ID=sa;Password=12345678;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
