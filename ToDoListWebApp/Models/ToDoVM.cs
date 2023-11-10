using ToDoListWebApp.Data;

namespace ToDoListWebApp.Models
{
    public class ToDoVM
    {
        public ToDoVM()
        {
            CurrentTask = new ToDo();
        }

        public Filter SearchFilters { get; set; }
        public List<Status> Statuses { get; set; }
        public List<Category> Categories { get; set; }

        public Dictionary<string, string> DueFilters { get; set; }
        public List<ToDo> Tasks { get; set; }

        public ToDo CurrentTask { get; set; }
    }
}
