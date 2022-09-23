using Ergie.Models;
namespace Ergie.Services
{
	public interface IDBService
	{
		public int GetHomeList();
		public void SetHomeList(int home);
		public bool SetDarkTheme(bool value);
		public bool UsingDarkTheme();

		public int AddTodoList(TodoList list);
		public List<TodoList> GetTodoLists();
		public TodoList GetList(int list_id);
		public int DeleteList(int list_id);

		public int AddItem(Item item);
		public void AddItem(ItemWithFlags item);
		public List<Item> GetItems(int list_id);
		public int DeleteItem(Item item);
		public void DeleteItem(ItemWithFlags item);
		public void UpdateItemComplete(Item item);

		public void AddItemFlag(ItemFlag flag);
		public void AddItemFlags(List<ItemFlag> flag);
		public List<ItemFlag> GetItemFlags(int item_id);
		public int DeleteItemFlag(ItemFlag flag);
		public void DeleteItemFlags(List<ItemFlag> flags);

		public void AddSavedFlag(Flag flag);
		public List<Flag> GetSavedFlags();
		public int DeleteSavedFlag(Flag flag);

	}
}
