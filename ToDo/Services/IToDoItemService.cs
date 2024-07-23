using ToDo.DTO;
using ToDo.Models;

namespace ToDo.Services
{
    public interface IToDoItemService
    {
        Task AddAsync(ToDoItemDto todoItemDto);
        Task<List<ToDoItem>> GetAllAsync();
        Task<List<ToDoItem>> GetByPriorityAsync(int level);
        Task<List<ToDoItem>> GetByStatusAsync(bool isCompleted);
        Task AddToUserAsync(ToDoItemDto item, int userId);
    }
}
