using Ergie.Database;
using Ergie.Models;
using System.Diagnostics;

namespace Ergie.Services
{
	public class DBService : IDBService
	{
		DBConnection db;
		public int HomeListId { get; set; } = 0;

		public void InitializeDatabaseTestingSetup()
		{
			db.Connection.Insert(new User { HomeListId = 1 });
			AddTodoList(new TodoList { Name = "My Todo List" });
			var item1 = new ItemWithFlags(new Item(1, "My Todo Item", "", false), new List<ItemFlag>{
				new ItemFlag(1, "MyTag", "#09F")
			});
			AddItem(item1);

			AddSavedFlag(new Flag
			{
				Name = "Flag1",
				Color = "#0F9"
			});

			Debug.WriteLine("Database Debug Information:");
			Debug.WriteLine("Home List Id: " + GetHomeList());

			DebugPrintUsers();
			DebugPrintLists();
			DebugPrintItems();
		}

		public DBService(DBConnection conn)
		{
			db = conn;
			//InitializeDatabaseTestingSetup();
		}

		public int GetHomeList()
		{
			var users = db.Connection.Table<User>().ToList();
			if (users.Count > 0)
			{
				HomeListId = users[0].HomeListId;
				return HomeListId;
			}
			return 0;
		}

		public void SetHomeList(int home)
		{
			HomeListId = home;
			var cmd = new SQLite.SQLiteCommand(db.Connection);
			cmd.CommandText = $"UPDATE user SET HomeListId = {home} WHERE _id = 1";
			cmd.ExecuteNonQuery();
		}

		public bool SetDarkTheme(bool value)
		{
			var cmd = new SQLite.SQLiteCommand(db.Connection);
			cmd.CommandText = $"UPDATE user SET DarkTheme = {value} WHERE _id = 1";
			return cmd.ExecuteNonQuery() == 1;
		}

		public bool UsingDarkTheme()
		{
			var user = db.Connection.Table<User>().Where(user => user.Id == 1).FirstOrDefault();
			if (user != null)
			{
				return user.DarkTheme;
			}
			return false;
		}

		public int AddTodoList(TodoList list)
		{
			int result = db.Connection.Insert(list);
			if (result >= 1)
			{
				return db.Connection.Table<TodoList>().Last().Id;
			}
			return 0;
		}

		public List<TodoList> GetTodoLists()
		{
			return db.Connection.Table<TodoList>().ToList();
		}

		public TodoList GetList(int list_id)
		{
			if(list_id <= 0) { return null; }
			return db.Connection.Table<TodoList>().Where(list => list.Id == list_id).First();
		}

		public int DeleteList(int list_id)
		{
			var items = GetItems(list_id);
			foreach(var item in items) {
				var flags = GetItemFlags(item.Id);
				DeleteItemFlags(flags);
				DeleteItem(item);
			}

			return db.Connection.Delete<TodoList>(list_id);
		}

		// Returns the ID of the last item added
		public int AddItem(Item item)
		{
			item.ListId = HomeListId;
			int result = db.Connection.Insert(item);
			if (result >= 1) {
				return db.Connection.Table<Item>().Last().Id;
			}
			return 0;
		}

		public void AddItem(ItemWithFlags item)
		{
			int itemId = AddItem(item.Item);
			if(itemId == 0) { return; }

			// Add the ItemId to all the Items flags
			foreach(var flag in item.Flags)
			{
				flag.ItemId = itemId;
			}

			AddItemFlags(item.Flags);
		}

		public List<Item> GetItems(int list_id)
		{
			var items = db.Connection.Table<Item>().Where(item => item.ListId == list_id).ToList();
			return items;
		}

		public int DeleteItem(Item item)
		{
			return db.Connection.Delete<Item>(item.Id);
		}

		public void DeleteItem(ItemWithFlags item)
		{
			DeleteItem(item.Item);
			DeleteItemFlags(item.Flags);
		}

		public void UpdateItemComplete(Item item)
		{
			var cmd = new SQLite.SQLiteCommand(db.Connection);
			cmd.CommandText = $"UPDATE item SET Complete = {item.Complete} WHERE _id = {item.Id}";
			cmd.ExecuteNonQuery();
		}

		public void AddItemFlag(ItemFlag flag)
		{
			db.Connection.Insert(flag);
		}

		public void AddItemFlags(List<ItemFlag> flags)
		{
			db.Connection.InsertAll(flags);
		}

		public List<ItemFlag> GetItemFlags(int item_id)
		{
			var flags = db.Connection.Table<ItemFlag>().Where(flag => flag.ItemId == item_id).ToList();
			return flags;
		}

		public int DeleteItemFlag(ItemFlag flag)
		{
			return db.Connection.Delete<ItemFlag>(flag.Id);
		}

		public void DeleteItemFlags(List<ItemFlag> flags)
		{
			foreach(var flag in flags)
			{
				DeleteItemFlag(flag);
			}
		}

		public void AddSavedFlag(Flag flag)
		{
			db.Connection.Insert(flag);
		}

		public List<Flag> GetSavedFlags()
		{
			return db.Connection.Table<Flag>().ToList();
		}

		public int DeleteSavedFlag(Flag flag)
		{
			return db.Connection.Delete<Flag>(flag.Id);
		}

		public void DebugPrintUsers()
		{
			foreach (var user in db.Connection.Table<User>().ToList())
			{
				Debug.WriteLine("User[ Id:" + user.Id + ", Home List Id: " + user.HomeListId + ", Using Dark Mode: " + user.DarkTheme + "]");
			}
		}

		public void DebugPrintLists()
		{
			foreach (var list in db.Connection.Table<TodoList>().ToList())
			{
				Debug.WriteLine("List[ Id:" + list.Id + ", Name: " + list.Name + "]");
			}
		}

		public void DebugPrintItems()
		{
			foreach (var item in db.Connection.Table<Item>().ToList())
			{
				Debug.WriteLine("Item[ Id:" + item.Id + ", List Id: " + item.ListId + ", Title: " + item.Title + "]");
			}
		}
	}
}
