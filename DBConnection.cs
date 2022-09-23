using SQLite;
using Ergie.Models;
using System.Diagnostics;

namespace Ergie.Database
{
	public class DBConnection
	{
		public SQLiteConnection Connection { get; set; }

		public DBConnection()
		{
			// Get our root directory.
			var dbFileDir = FileSystem.Current.AppDataDirectory;

			// Connect to the database file.
			var dbFile = Path.Combine(FileSystem.AppDataDirectory, "data.db");
			//File.Delete(dbFile); // Reset the database file while debugging
			Connection = new SQLiteConnection(dbFile);
			// Create and or verify our tables.
			Connection.CreateTables<User, Flag, TodoList, Item, ItemFlag>();

			if (Connection.Table<User>().Where(user => user.Id == 1).FirstOrDefault() == null)
			{
				Connection.Insert(new User
				{
					HomeListId = 0,
					DarkTheme = false
				});
			}
		}
	}
}
