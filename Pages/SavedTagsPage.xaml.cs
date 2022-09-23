using Ergie.Helper;
using Ergie.Models;
using Ergie.ViewModels;
using System.Diagnostics;

namespace Ergie.Pages
{
	public partial class SavedTagsPage : ContentPage, IQueryAttributable
	{
		public FlagViewModel FlagViewModel { get; }

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

		public SavedTagsPage()
		{
			InitializeComponent();
			FlagViewModel = ServiceHelper.GetService<FlagViewModel>();
			FlagViewModel.LoadSavedFlags();
			BindingContext = FlagViewModel;
		}

		async void PickColor(object sender, EventArgs args)
		{
			await Shell.Current.GoToAsync("pickcolor");
		}

		public void DeleteSavedFlag(object sender, EventArgs args)
		{
			Button btn = sender as Button;
			var flag = btn.CommandParameter as Flag;
			FlagViewModel.DeleteSavedFlag(flag);
		}

		void AddCustomFlag(object sender, EventArgs args)
		{
			var cFlagName = CustomFlagName.Text;
			if (cFlagName.Length > 0)
			{
				FlagViewModel.CreateSavedFlag(new()
				{
					Name = cFlagName,
					Color = CustomFlagColorBtn.BackgroundColor.ToHex()
				});
				CustomFlagName.Text = "";
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