using System.Globalization;

using ToDo.Database;
using ToDo.Types;

namespace ToDo.ConsoleApp;
public class Program
{

	public static void Main(string[] args)
	{
		Console.WriteLine("To show help enter /help");

		while (true)
		{
			Console.Write(">> ");
			string? command = Console.ReadLine();

			switch (command) 
			{
				case "/help":
					Help();
					break;

				case "/list":
					List();
					break;

				case "/edit":
					Edit();
					break;

				case "/delete":
					Delete();
					break;

				case "/new":
					New();
					break;

				case "/exit":
					Environment.Exit(0);
					break;

				case null:
					continue;

				default:
					Console.WriteLine("Unknown command, enter /help to get commands list");
					break;
			}
		}
	} 

	public static void Help()
	{
		string help = 
		"/help - Display this message\n" + 
		"/list - Lists your tasks\n" +
		"/edit - Edit task\n" +
		"/delete - Delete task\n" +
		"/new - Create new task";

		Console.WriteLine(help);
	}

	public static void List()
	{
		string username = Environment.UserName;

		using var db = new ToDoDbContext();
		db.Database.EnsureCreated();

		List<Item> tasks = db.Items
							.Where(x => x.User == username)
							.ToList();

		if (tasks.Count == 0) 
		{
			Console.WriteLine("No tasks for this user.");
			return;
		}

		foreach (Item task in tasks) 
		{
			string output = 
			$"{task.Id}: {task.Name}\n" +
			$"Description: {task.Description}\n" +
			$"Completed: {(task.Completed ? "True" : "False")}\n" +
			$"Start: {task.Start.ToLocalTime().ToString()}\n" +
			$"End: {task.Start.ToLocalTime().ToString()}\n\n";

			Console.WriteLine(output);
		}
	}

	public static void Edit()
	{
		string username = Environment.UserName;

		Console.Write("Enter task id: ");
		string? stringId = Console.ReadLine();
		int id;

		bool parseSuccess = int.TryParse(stringId, out id);

		if (!parseSuccess) 
		{
			Console.WriteLine("Unable to parse task id");
			return;
		}

		using var db = new ToDoDbContext();
		db.Database.EnsureCreated();

		Item? task = db.Items
						.Where(x => x.User == username)
						.Where(x => x.Id == id)
						.FirstOrDefault();

		if (task is null) 
		{
			Console.WriteLine("Unable to find task with this id");
			return;
		}

		Console.Write("Task name (set empty for previous): ");
		string? name = Console.ReadLine();

		if (name != "" && name != null) 
		{
			task.Name = name;
		}

		Console.Write("Task description (optional, set empty for previous): ");
		string? description = Console.ReadLine();

		if (description != "" && description != null) 
		{
			task.Description = description;
		}

		Console.Write("Task completion state (true/false, set empty for previous): ");
		string? stringBool = Console.ReadLine();
		bool completed;

		if (stringBool != "" && stringBool != null) 
		{
			bool boolParseSuccess = bool.TryParse(stringBool, out completed);

			if (boolParseSuccess) 
			{
				task.Completed = completed;
			}
			
			if (!boolParseSuccess) 
			{
				Console.WriteLine("Unable to parse bool, expecting \"false\"");
			}
		}

		CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

		Console.Write("Task start time (format is \"day/month/year hour:minute:second\", enter to set previous time): ");
		string? startString = Console.ReadLine();
		DateTime start;

		bool startParseSuccess = DateTime.TryParseExact(startString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out start);

		if (startString != "" && startString != null) 
		{
			task.Start = start;
		}

		Console.Write("Task end time (format is \"day/month/year hour:minute:second\", enter to set previous time): ");
		string? endString = Console.ReadLine();
		DateTime end;

		bool endParseSuccess = DateTime.TryParseExact(endString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out end);

		if (endString != "" && endString != null) 
		{
			task.End = end;
		}

		db.Items.Update(task);

		db.SaveChanges();
	}

	public static void Delete()
	{
		string username = Environment.UserName;

		Console.Write("Enter task id: ");
		string? stringId = Console.ReadLine();
		int id;

		bool parseSuccess = int.TryParse(stringId, out id);

		if (!parseSuccess) 
		{
			Console.WriteLine("Unable to parse task id");
			return;
		}

		using var db = new ToDoDbContext();
		db.Database.EnsureCreated();

		Item? task = db.Items
						.Where(x => x.User == username)
						.Where(x => x.Id == id)
						.FirstOrDefault();

		if (task is null) 
		{
			Console.WriteLine("Unable to find task with this id");
			return;
		}

		Console.Write($"Are you sure you want to delete task {id} (y/N)?: ");
		var answer = Console.ReadLine();

		switch (answer) 
		{
			case "Y": goto case "y";
			case "y":
				db.Items.Remove(task);
				db.SaveChanges();

				Console.WriteLine($"Deleted task {task.Id}.");
				break;

			case "N": goto case "n";
			case "n":
				Console.WriteLine("Cancelled task deleting.");
				break;

			default:
				Console.WriteLine("Unknown answer, expecting as \"No\"");
				break;
		}
	}

	public static void New()
	{
		string username = Environment.UserName;

		using var db = new ToDoDbContext();
		db.Database.EnsureCreated();

		Console.Write("Task name: ");
		string? name = Console.ReadLine();

		if (name is null) 
		{
			Console.WriteLine("Name cannot be null!");
			return;
		}

		Console.Write("Task description (optional): ");
		string? description = Console.ReadLine();

		CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

		Console.Write("Task start time (format is \"day/month/year hour:minute:second\", enter to set current time): ");
		string? startString = Console.ReadLine();
		DateTime start;

		bool startParseSuccess = DateTime.TryParseExact(startString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out start);

		if (startString == null | startString == "") 
		{
			start = DateTime.UtcNow;
			startParseSuccess = true;
		}

		if (!startParseSuccess) 
		{
			Console.WriteLine("Failed to parse time! Setting current time as start time.");
			start = DateTime.Now;
		}

		Console.Write("Task end time (format is \"day/month/year hour:minute:second\", enter to set current time): ");
		string? endString = Console.ReadLine();
		DateTime end;

		bool endParseSuccess = DateTime.TryParseExact(endString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out end);

		if (endString == null | endString == "") 
		{
			end = DateTime.UtcNow;
			endParseSuccess = true;
		}

		if (!endParseSuccess) 
		{
			Console.WriteLine("Failed to parse time! Setting current time as start time.");
			end = DateTime.Now;
		}

		int lastId;
		var userItems = db.Items
					.Where(x => x.User == username);
	
		if (userItems.Count() == 0) 
		{
			lastId = 0;
		}
		else 
		{
			lastId = userItems
						.OrderByDescending(x => x.Id)
						.Select(x => x.Id)
						.FirstOrDefault();
		}

		db.Items.Add(
			new Item 
			{
				User = username,
				Id = ++lastId,
				Name = name,
				Description = description,
				Start = start.ToUniversalTime(),
				End = end.ToUniversalTime(),
			}
		);

		db.SaveChanges();
	}
}