using Newtonsoft.Json;
using SQLite;

namespace Ergie.Models
{
	//public class Flag : IEquatable<Flag>
	//{
	//	[JsonProperty("color")]
	//	public string Color { get; set; }

	//	[JsonProperty("name")]
	//	public string Name { get; set; }

	//	public bool Equals(Flag other)
	//	{
	//		return Name == other.Name;
	//	}
	//}

	[Table("flag")]
	public class Flag
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }

		[MaxLength(10), Unique]
		public string Name { get; set; }

		[MaxLength(7)]
		public string Color { get; set; }

		public Flag() { }
		public Flag(string name, string color)
		{
			Name = name;
			Color = color;
		}
	}
}
