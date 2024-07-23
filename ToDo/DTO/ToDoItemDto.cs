﻿namespace ToDo.DTO
{
    public class ToDoItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public int PriorityId { get; set; }
        public int UserId { get; set; }
    }
}
