using Microsoft.EntityFrameworkCore;

namespace ToDoListWebApp.Data
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext(DbContextOptions<ToDoListContext> options)
     : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<ToDo> ToDos { get; set; }

    }
}
