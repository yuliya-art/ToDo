using ToDo.Models.Data;
using ToDo.DTO;
using ToDo.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace ToDo.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly ToDoContext _context;
        private readonly ILogger<ToDoItemService> _logger;
        public ToDoItemService(ToDoContext context, ILogger<ToDoItemService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(ToDoItemDto todoItemDto)
        {
            if (!IsRightPriority(todoItemDto.PriorityId))
            {
                _logger.LogError($"Priority with id {todoItemDto.PriorityId} is not right");
                throw new InvalidOperationException();
            }
            if (!IsRightUser(todoItemDto.UserId))
            {
                _logger.LogError($"User with id {todoItemDto.UserId} is not right");
                throw new InvalidOperationException();
            }
            try
            {
                var item = new ToDoItem()
                {
                    Title = todoItemDto.Title,
                    Description = todoItemDto.Description,
                    DueDate = todoItemDto.DueDate,
                    IsCompleted = todoItemDto.IsCompleted,
                    PriorityId = todoItemDto.PriorityId,
                    UserId = todoItemDto.UserId
                };
                await _context.ToDoItems.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError("Database error while adding item", dbEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while adding item", ex.Message);
                throw;
            }
        }

        public async Task<List<ToDoItem>> GetAllAsync()
        {
            var items = await _context.ToDoItems
                                    .Include(x => x.Priority)
                                    .Include(x => x.User)
                                    .ToListAsync();
            if (items == null)
            {
                _logger.LogError("List is null");
                throw new NullReferenceException(nameof(items));
            }
            if (items.Count == 0) 
            {
                _logger.LogInformation("Items is empty");
            }
            return items;
        }

        public async Task<List<ToDoItem>> GetByPriorityAsync(int level)
        {
            try
            {
                var priority = await _context.Priorities.FirstOrDefaultAsync(x => x.Level == level);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Priority with level {level} don't exists", ex.Message);
                throw;
            }
            return await _context.ToDoItems
                            .Where(x => x.Priority.Level == level)
                            .Include(x => x.Priority)
                            .Include(x => x.User)
                            .ToListAsync();
        }

        public async Task<List<ToDoItem>> GetByStatusAsync(bool isCompleted)
        {
            return await _context.ToDoItems
                            .Where(x => x.IsCompleted == isCompleted)
                            .Include(x => x.Priority)
                            .Include(x => x.User)
                            .ToListAsync();
        }

        public async Task AddToUserAsync(ToDoItemDto todoItem, int userId)
        {
            if (!IsRightUser(userId))
            {
                _logger.LogError($"User with id {userId} is not right");
                throw new InvalidOperationException();
            }

            ToDoItem item = new ToDoItem();
            try
            {
                item = _context.ToDoItems.FirstOrDefault(x => x.Id == todoItem.Id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in changing user of item with id {todoItem.Id}", e.Message);
                throw;
            }
            if (item == null)
            {
                _logger.LogError($"Item with id {todoItem.Id} doesn't exist");
                throw new ArgumentNullException(nameof(todoItem));
            }
            item.UserId = userId;
            _context.ToDoItems.Update(item);
            await _context.SaveChangesAsync();
        }

        private bool IsRightPriority(int id)
        {
            var priority = _context.Priorities.FirstOrDefault(x => x.Id == id);

            return priority != null;
        }

        private bool IsRightUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            return user != null;
        }
    }
}
