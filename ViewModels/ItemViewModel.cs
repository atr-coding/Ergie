using Ergie.Models;
using Ergie.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Ergie.ViewModels
{
	public class ItemViewModel : BaseViewModel
	{
		public DBService DBService { get; }
		public ObservableCollection<ItemWithFlags> Items { get; } = new();

		string todoListTitle = "";
		bool showDeleteModeButton = false;
		bool showDeleteButtons = false;
		bool showCheckBoxes = true;
		bool showTitleGrid = false;
		string emptyViewText = "Nothing to-do.";
		bool emptyViewTextVisible = true;

		public bool EmptyViewTextVisible
		{
			get { return emptyViewTextVisible; }
			set
			{
				if(emptyViewTextVisible != value)
				{
					emptyViewTextVisible = value;
					OnPropertyChanged(nameof(EmptyViewTextVisible));
				}
			}
		}

		public string EmptyViewText
		{
			get
			{
				return emptyViewText;
			}
			set
			{
				if (emptyViewText != value)
				{
					emptyViewText = value;
					OnPropertyChanged(nameof(EmptyViewText));
				}
			}
		}

		public bool ShowTitleGrid
		{
			get
			{
				return showTitleGrid;
			}
			set
			{
				if(showTitleGrid != value)
				{
					showTitleGrid = value;
					OnPropertyChanged(nameof(ShowTitleGrid));
				}
			}
		}

		public string TodoListTitle
		{
			get { return todoListTitle; }
			set
			{
				if (todoListTitle != value)
				{
					todoListTitle = value;
					OnPropertyChanged(nameof(TodoListTitle));
				}
			}
		}

		public bool ShowDeleteModeButton
		{
			get { return showDeleteModeButton; }
			set
			{
				if (showDeleteModeButton != value)
				{
					showDeleteModeButton = value;
					OnPropertyChanged(nameof(ShowDeleteModeButton));
				}
			}
		}

		public bool ShowDeleteButtons
		{
			get { return showDeleteButtons; }
			set
			{
				if (showDeleteButtons != value)
				{
					showDeleteButtons = value;
					OnPropertyChanged(nameof(ShowDeleteButtons));
				}
			}
		}

		public bool ShowCheckBoxes
		{
			get { return showCheckBoxes; }
			set
			{
				if (showCheckBoxes != value)
				{
					showCheckBoxes = value;
					OnPropertyChanged(nameof(ShowCheckBoxes));
				}
			}
		}

		public ICommand DeleteCommand => new Command<ItemWithFlags>(DeleteItem);

		public ItemViewModel(DBService dbService)
		{
			DBService = dbService;
		}

		public void AddItem(ItemWithFlags Item)
		{
			if (DBService.GetHomeList() != 0)
			{
				DBService.AddItem(Item);
				Items.Add(Item);
				ShowDeleteModeButton = true;
				EmptyViewTextVisible = false;
				Debug.Write("\n\n");
				DBService.DebugPrintItems();
				Debug.Write("\n\n");
			}
		}

		public void DeleteItem(ItemWithFlags item)
		{
			DBService.DeleteItem(item.Item);
			Items.Remove(item);
			if (Items.Count == 0)
			{
				ShowDeleteButtons = false;
				ShowCheckBoxes = true;
				ShowDeleteModeButton = false;
				EmptyViewText = "Nothing to-do.";
				EmptyViewTextVisible = true;
			}
		}

		public void LoadItems(int list_id)
		{
			TodoList list = DBService.GetList(list_id);

			DBService.SetHomeList(list_id);
			if (list == null || list_id == 0)
			{
				Items.Clear();
				TodoListTitle = "";
				ShowDeleteModeButton = false;
				ShowTitleGrid = false;
				ShowDeleteButtons = false;
				ShowCheckBoxes = true;
				EmptyViewText = "No list selected.";
				EmptyViewTextVisible = true;

				return;
			}

			TodoListTitle = list.Name;
			ShowTitleGrid = true;
			ShowDeleteButtons = false;
			ShowCheckBoxes = true;

			var items = DBService.GetItems(list_id);

			if(items.Count == 0)
			{
				ShowDeleteModeButton = false;
				EmptyViewText = "Nothing to-do.";
				EmptyViewTextVisible = true;
			} else
			{
				ShowDeleteModeButton = true;
				EmptyViewTextVisible = false;
			}

			Items.Clear();

			foreach (var item in items)
			{
				// Add flags container to the Item.
				ItemWithFlags iwf = new(item);

				// Get the flags for the Item.
				var flags = DBService.GetItemFlags(iwf.Item.Id);
				foreach (var flag in flags)
				{
					//// Convert from ItemFlag to Flag.
					iwf.Flags.Add(new ItemFlag
					{
						ItemId = item.Id,
						Name = flag.Name,
						Color = flag.Color
					});
				}

				// Update our collection.
				Items.Add(iwf);
			}
		}
	}
}