using Newtonsoft.Json;
using SQLite;

namespace Ergie.Models
{
	//public class TodoList
	//{
	//	[JsonProperty("name")]
	//	public string Name { get; set; }

	//	[JsonProperty("items")]
	//	public List<Item> Items { get; set; }
	//}

	[Table("list")]
	public class TodoList
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }

		[MaxLength(256), Unique]
		public string Name { get; set; }

		public TodoList() { }
		public TodoList(string name)
		{
			Name = name;
		}
	}

	public class TodoListWithItems : TodoList
	{
		public List<ItemWithFlags> Items { get; set; }
	}
}
