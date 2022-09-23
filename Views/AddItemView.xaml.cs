using Ergie.Helper;
using Ergie.Models;
using Ergie.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Ergie.Views
{
	public partial class AddItemView : ContentPage, IQueryAttributable
	{
		ObservableCollection<ItemFlag> FlagList = new();
		public FlagViewModel FlagViewModel { get; set; }

		string selectedColor;
		public string SelectedColor
		{
			get => selectedColor;
			set
			{
				selectedColor = "#" + value;
				CustomFlagColorBtn.BackgroundColor = Color.FromArgb(value);
			}
		}

		public AddItemView()
		{
			InitializeComponent();
			FlagViewModel = ServiceHelper.GetService<FlagViewModel>();
			FlagViewModel.LoadSavedFlags();
			BindingContext = FlagViewModel;
			FlagListView.ItemsSource = FlagList;
		}

		void RemoveFlag(object sender, EventArgs args)
		{
			Button btn = sender as Button;
			var flag = btn.CommandParameter as ItemFlag;

			if (FlagList.Contains(flag))
			{
				FlagList.Remove(flag);
			}
		}

		void AddFlag(object sender, EventArgs args)
		{
			Button btn = sender as Button;
			var flag = btn.CommandParameter as Flag;

			ItemFlag itemFlag = new()
			{
				Name = flag.Name,
				Color = flag.Color
			};

			if (!FlagList.Contains(itemFlag))
			{
				FlagList.Add(itemFlag);
			}
		}

		void TitleChanged(object sender, EventArgs args)
		{
			Entry ent = sender as Entry;
			//Debug.WriteLine("Current Home Page: " + FlagViewModel.DBService.GetHomeList());
			if (ent.Text.Length > 0 && FlagViewModel.DBService.GetHomeList() != 0)
			{
				Title = ent.Text;
				AddItemButton.BackgroundColor = (Color)Application.Current.Resources.MergedDictionaries.First()["ButtonEnabledColor"];
				AddItemButton.IsEnabled = true;
			}
			else
			{
				Title = "";
				AddItemButton.BackgroundColor = (Color)Application.Current.Resources.MergedDictionaries.First()["ButtonDisabledColor"];
				AddItemButton.IsEnabled = false;
			}
		}

		async void CreateItem(object sender, EventArgs args)
		{
			ItemWithFlags item = new ItemWithFlags();
			item.Item.Title = ItemTitle.Text;
			item.Item.Description = ItemDesc.Text;
			item.Item.Complete = false;
			item.Item.Id = 0;
			item.Item.ListId = 0;
			item.Flags = FlagList.ToList();
			var navParam = new Dictionary<string, object> { { "add", item } };
			await Shell.Current.GoToAsync("//Home", navParam);
			Clear();
		}

		async void PickColor(object sender, EventArgs args)
		{
			await Shell.Current.GoToAsync("pickcolor");
		}

		void AddCustomFlag(object sender, EventArgs args)
		{
			var cFlagName = CustomFlagName.Text;
			if (cFlagName.Length > 0)
			{
				ItemFlag flag = new()
				{
					Name = cFlagName,
					Color = CustomFlagColorBtn.BackgroundColor.ToHex()
				};

				if (!FlagList.Contains(flag))
				{
					FlagList.Add(flag);

					CustomFlagAddBtn.IsEnabled = false;
					CustomFlagAddBtn.BackgroundColor = Colors.Red;
					CustomFlagName.Text = "";
					CustomFlagColorBtn.BackgroundColor = Color.FromArgb("#0099FF");
				}
			}
		}

		void CustomFlagNameChanged(object sender, EventArgs args)
		{
			var text = CustomFlagName.Text;
			if (text?.Length > 0)
			{
				CustomFlagAddBtn.BackgroundColor = (Color)Application.Current.Resources.MergedDictionaries.First()["ButtonEnabledColor"];
				CustomFlagAddBtn.IsEnabled = true;
			}
			else
			{
				CustomFlagAddBtn.BackgroundColor = (Color)Application.Current.Resources.MergedDictionaries.First()["ButtonDisabledColor"];
				CustomFlagAddBtn.IsEnabled = false;
			}
		}

		void Clear()
		{
			FlagList.Clear();

			ItemTitle.Text = "";
			ItemDesc.Text = "";
			CustomFlagName.Text = "";

			AddItemButton.IsEnabled = false;
			AddItemButton.BackgroundColor = (Color)Application.Current.Resources.MergedDictionaries.First()["ButtonDisabledColor"];

			CustomFlagAddBtn.IsEnabled = false;
			CustomFlagAddBtn.BackgroundColor = (Color)Application.Current.Resources.MergedDictionaries.First()["ButtonDisabledColor"];
		}

		protected override void OnNavigatedTo(NavigatedToEventArgs args)
		{
			base.OnNavigatedTo(args);
		}

		public void ApplyQueryAttributes(IDictionary<string, object> query)
		{
			if (query.ContainsKey("selectedColor"))
			{
				SelectedColor = query["selectedColor"] as string;
			}
		}
	}
}