using Ergie.Helper;
using Ergie.Models;
using Ergie.Services;
using Ergie.ViewModels;
using System.Diagnostics;

//var navParam = new Dictionary<string, object> { { "item", item } };
//await Shell.Current.GoToAsync("viewitem", navParam);

namespace Ergie.Views
{
	public partial class ItemView : ContentPage, IQueryAttributable
	{
		public ItemViewModel ItemViewModel { get; set; }
		int homeListId = 0;

		public ItemView()
		{
			InitializeComponent();

			bool usingDarkTheme = ServiceHelper.GetService<DBService>().UsingDarkTheme();
			if (usingDarkTheme)
			{
				ThemeHelper.SetDarkTheme(true);
			}

			ItemViewModel = ServiceHelper.GetService<ItemViewModel>();
			BindingContext = ItemViewModel;
			homeListId = ServiceHelper.GetService<DBService>().GetHomeList();
			ItemViewModel.LoadItems(homeListId);
		}

		public void DeleteModeToggle(object sender, EventArgs args)
		{
			ItemViewModel.ShowDeleteButtons = !ItemViewModel.ShowDeleteButtons;
			ItemViewModel.ShowCheckBoxes = !ItemViewModel.ShowCheckBoxes;
		}

		protected override void OnNavigatedTo(NavigatedToEventArgs args)
		{
			base.OnNavigatedTo(args);
		}

		public void ApplyQueryAttributes(IDictionary<string, object> query)
		{
			if (query.ContainsKey("add"))
			{
				ItemWithFlags item = query["add"] as ItemWithFlags;
				ItemViewModel.AddItem(item);
			}
			else if (query.ContainsKey("listId"))
			{
				int id = int.Parse(query["listId"] as string);

				if (homeListId != id)
				{
					homeListId = id;
					ItemViewModel.LoadItems(homeListId);
				}
			}
			query.Clear();
		}

		public async void GoToItemViewPage(object sender, EventArgs args)
		{
			var item = (sender as Button).CommandParameter as ItemWithFlags;
			var navParam = new Dictionary<string, object> { { "item", item } };
			await Shell.Current.GoToAsync("viewitem", navParam);
		}

		private void CompleteItem(object sender, CheckedChangedEventArgs e)
		{
			var cb = sender as CheckBox;
			var item = cb.BindingContext as ItemWithFlags;
			item.Item.Complete = e.Value;
			ItemViewModel.DBService.UpdateItemComplete(item.Item);
		}
	}
}