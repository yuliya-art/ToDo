using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ToDo.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public IEnumerable<ToDoItem> ToDoItems { get; set; }
    }
}