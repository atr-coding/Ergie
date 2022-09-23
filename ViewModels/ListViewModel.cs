using Ergie.Models;
using Ergie.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Ergie.ViewModels
{
	public class ListViewModel : BaseViewModel
	{
		public ObservableCollection<TodoList> TodoLists { get; } = new();
		public DBService DBService { get; }
		public ICommand DeleteCommand => new Command<TodoList>(DeleteList);
		public ItemViewModel ItemViewModel { get; }

		bool showDeleteButtons = false;
		public bool ShowDeleteButtons
		{
			get { return showDeleteButtons; }
			set
			{
				if(showDeleteButtons != value)
				{
					showDeleteButtons = value;
					OnPropertyChanged(nameof(ShowDeleteButtons));
				}
			}
		}

		public ListViewModel(ItemViewModel itemViewModel, DBService dbService)
		{
			DBService = dbService;
			ItemViewModel = itemViewModel;
		}

		public void LoadTodoLists()
		{
			// Load our todo lists
			var todoLists = DBService.GetTodoLists();

			// Clear the old list
			TodoLists.Clear();

			// If we have any lists then add them to our collection
			if (todoLists.Count != 0)
			{
				foreach (var list in todoLists)
				{
					TodoLists.Add(list);
				}
			}
		}

		public void AddTodoList(TodoList list)
		{
			int listId = DBService.AddTodoList(list);
			list.Id = listId;
			TodoLists.Add(list);
		}

		public void DeleteList(TodoList list)
		{
			DBService.DeleteList(list.Id);
			TodoLists.Remove(list);

			if (DBService.HomeListId == list.Id && TodoLists.Count > 0)
			{
				// Easy way of clearing the Item View if we delete all the lists
				ItemViewModel.LoadItems(0);
			}

			if (TodoLists.Count == 0)
			{
				ShowDeleteButtons = false;
				ItemViewModel.LoadItems(0);
			}
		}
	}
}
