using System.ComponentModel.DataAnnotations;

namespace ToDoListWebApp.Data
{
    public class ToDo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a due date")]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required(ErrorMessage = "Please select a status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

    }
}
