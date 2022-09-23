using Newtonsoft.Json;
using SQLite;

namespace Ergie.Models
{
	//public class Item
	//{
	//	[JsonProperty("complete")]
	//	public bool Complete { get; set; }

	//	[JsonProperty("title")]
	//	public string Title { get; set; }

	//	[JsonProperty("description")]
	//	public string Description { get; set; }

	//	[JsonProperty("flags")]
	//	public List<Flag> Flags { get; set; }
	//}

	[Table("item")]
	public class Item
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }

		[Indexed]
		public int ListId { get; set; }

		[MaxLength(256)]
		public string Title { get; set; }

		[MaxLength(256)]
		public string Description { get; set; }

		public bool Complete { get; set; }

		public Item() {}
		public Item(int listId, string title, string description, bool complete)
		{
			ListId = listId;
			Title = title;
			Description = description;
			Complete = complete;
		}
	}

	public class ItemWithFlags
	{
		public Item Item { get; set; }

		public List<ItemFlag> Flags { get; set; } = new();

		public ItemWithFlags()
		{
			Item = new();
		}

		public ItemWithFlags(Item item)
		{
			Item = item;
		}

		public ItemWithFlags(Item item, List<ItemFlag> flags)
		{
			Item = item;
			Flags = flags;
		}
	}
}
