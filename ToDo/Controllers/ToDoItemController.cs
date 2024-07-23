using Microsoft.AspNetCore.Mvc;
using ToDo.DTO;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoItemController : ControllerBase
    {
        private readonly ILogger<ToDoItemController> _logger;
        private readonly IToDoItemService _todoItemService;

        public ToDoItemController(IToDoItemService todoItemService, ILogger<ToDoItemController> logger)
        {
            _todoItemService = todoItemService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItem>> Post([FromBody]ToDoItemDto todoItemDto)
        {
           await _todoItemService.AddAsync(todoItemDto);
            return Ok(todoItemDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> Get()
        {
            return Ok(await _todoItemService.GetAllAsync());
        }

        [HttpGet("byPriority")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetByUser([FromQuery] int level = 1)
        {
            return Ok(await _todoItemService.GetByPriorityAsync(level));
        }

        [HttpGet("byStatus")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetByStatus([FromQuery] bool isCompleted = true)
        {
            return Ok(await _todoItemService.GetByStatusAsync(isCompleted));
        }

        [HttpPut("toUser")]
        public async Task<ActionResult<ToDoItem>> AddToUser(ToDoItemDto todoItem, int userId)
        {
            await _todoItemService.AddToUserAsync(todoItem, userId);
            return Ok(todoItem);
        }
    }
}
