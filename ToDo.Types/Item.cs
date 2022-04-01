using System;

namespace ToDo.Types;

/// <summary>
/// Class <c>Item</c> is implementation of ToDo item
/// </summary>
public class Item
{

#nullable disable warnings
	/// <summary>
	/// <c>User</c> property is a name of user has this task
	/// </summary>
	public string User { get; set; }
#nullable restore warnings

	/// <summary>
	/// <c>Id</c> property is a id of <c>Item</c>
	/// </summary>
	public int Id { get; set; }

#nullable disable warnings
	/// <summary>
	/// <c>Name</c> property is a <c>Item</c> name
	/// </summary>
	public string Name { get; set; }
#nullable restore warnings

	/// <summary>
	/// <c>Description</c> property is a <c>Item</c> description
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// <c>Completed</c> property is a <c>Item</c> completion state
	/// </summary>
	public bool Completed { get; set; }

	/// <summary>
	/// <c>Start</c> property is a <c>Item</c> completion start time in UTC format
	/// </summary>
	public DateTime Start { get; set; }

	/// <summary>
	/// <c>End</c> property is a <c>Item</c> completion end time in UTC format a.k.a "Deadline"
	/// </summary>
	public DateTime End { get; set; }


	// /// <summary>
	// /// <c>Item</c> method is a <c>Item</c> constructor
	// /// </summary>
	// /// <param name="name">Item name</param>
	// /// <returns></returns>
	// public Item(string name)
	// 	: this(name, "", false, DateTime.Now, DateTime.Now.AddDays(1)) {}

	// /// <summary>
	// /// <c>Item</c> method is a <c>Item</c> constructor
	// /// </summary>
	// /// <param name="name">Item name</param>
	// /// <param name="description">Item description</param>
	// /// <returns></returns>
	// public Item(string name, string description)
	// 	: this(name, description, false, DateTime.Now, DateTime.Now.AddDays(1)) {}

	// /// <summary>
	// /// <c>Item</c> method is a <c>Item</c> constructor
	// /// </summary>
	// /// <param name="name">Item name</param>
	// /// <param name="description">Item description</param>
	// /// <param name="completed">Item completion state</param>
	// /// <returns></returns>
	// public Item(string name, string description, bool completed)
	// 	: this(name, description, completed, DateTime.Now, DateTime.Now.AddDays(1)) {}

	// /// <summary>
	// /// <c>Item</c> method is a <c>Item</c> construtor
	// /// </summary>
	// /// <param name="name">Item name</param>
	// /// <param name="description">Item description</param>
	// /// <param name="completed">Item completion state</param>
	// /// <param name="start">Item start time</param>
	// /// <returns></returns>
	// public Item(string name, string description, bool completed, DateTime start)
	// 	: this(name, description, completed, start, start.AddDays(1)) {}		
	
	// /// <summary>
	// /// <c>Item</c> method is a <c>Item</c> constructor
	// /// </summary>
	// /// <param name="name">Item name</param>
	// /// <param name="description">Item description</param>
	// /// <param name="completed">Item completion state</param>
	// /// <param name="start">Item completion start time</param>
	// /// <param name="end">Item completion end time a.k.a "Deadline"</param>
	// /// <returns></returns>
	// public Item(string name, string description, bool completed, DateTime start, DateTime end)
	// {
	// 	Name = name;
	// 	Description = description;

	// 	Completed = completed;

	// 	Start = start;
	// 	End = End;
	// }
}