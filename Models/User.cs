using Newtonsoft.Json;
using SQLite;

namespace Ergie.Models
{
	//public class User
	//{
	//	[JsonProperty("home")]
	//	public string Home { get; set; }

	//	[JsonProperty("flags")]
	//	public List<Flag> Flags { get; set; }

	//	[JsonProperty("todolists")]
	//	public List<TodoList> Todolists { get; set; }
	//}

	[Table("user")]
	public class User
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }

		public int HomeListId { get; set; }

		public bool DarkTheme { get; set; }
	}
}
