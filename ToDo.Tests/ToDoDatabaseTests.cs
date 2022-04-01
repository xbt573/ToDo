using Xunit;
using FluentAssertions;
using ToDo.Database;
using ToDo.Types;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ToDo.Tests;

public class ToDoDatabaseTests
{
    [Fact]
    public void Test()
    {
        using var db = new ToDoDbContext();
        db.Database.EnsureCreated();

        Item sampleTask = CreateSampleTask();

        db.Items.Add(
            sampleTask
        );
        db.SaveChanges();

        Item task = db.Items
                        .Where(x => x.User == "xbt573")
                        .First();

        db.Items.Remove(task);
        db.SaveChanges();

        task.Should().Be(sampleTask);
    }

    public Item CreateSampleTask()
    {
        Item sampleItem = new Item
        {
            User = "xbt573",
            Id = 1,
            Name = "Do the tests!",
            Description = "Testing lib is XUnit",
            Completed = false,
            Start = DateTime.UtcNow,
            End = DateTime.UtcNow.AddDays(1)
        };

        return sampleItem;
    }
}