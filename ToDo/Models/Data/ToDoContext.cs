using Microsoft.EntityFrameworkCore;

namespace ToDo.Models.Data
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<User> Users { get; set; }

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "bfd" },
                new User { Id = 2, Name = "New" },
                new User { Id = 3, Name = "Year" }
            );
            modelBuilder.Entity<Priority>().HasData(
                new Priority { Id = 1, Level = 1 },
                new Priority { Id = 2, Level = 2 },
                new Priority { Id = 3, Level = 3 }
            );
        }
    }
}
