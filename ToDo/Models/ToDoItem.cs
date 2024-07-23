using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDo.Models
{
    public class ToDoItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }

        [JsonIgnore]
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
