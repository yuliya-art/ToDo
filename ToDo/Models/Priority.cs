using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ToDo.Models
{
    public class Priority
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Level { get; set; }
        [JsonIgnore]
        public IEnumerable<ToDoItem> ToDoItems { get; set; }
    }
}