using Microsoft.EntityFrameworkCore;
using ToDo.Types;

namespace ToDo.Database;
public class ToDoDbContext : DbContext
{
#nullable disable warnings
    public DbSet<Item> Items { get; set; }
#nullable restore warnings

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .HasKey(c => new {c.User, c.Id});
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql(@"Host=localhost;Username=postgres;Password=postgres;Database=ToDoDatabase");
}
