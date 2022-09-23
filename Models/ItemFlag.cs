using SQLite;

namespace Ergie.Models
{
	[Table("item_flag")]
	public class ItemFlag
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }

		[Indexed]
		public int ItemId { get; set; }

		[MaxLength(10)]
		public string Name { get; set; }

		[MaxLength(7)]
		public string Color { get; set; }

		public ItemFlag() { }
		public ItemFlag(int itemId, string name, string color)
		{
			ItemId = itemId;
			Name = name;
			Color = color;
		}

		public Flag Flag()
		{
			return new Flag()
			{
				Name = Name,
				Color = Color
			};
		}
	}
}
