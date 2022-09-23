using Ergie.Helper;
using Ergie.Services;
using Ergie.Themes;

namespace Ergie.Views
{
	public partial class SettingsView : ContentPage
	{
		public SettingsView()
		{
			InitializeComponent();
			ServiceHelper.GetService<DBService>().DebugPrintUsers();
			bool darkMode = ServiceHelper.GetService<DBService>().UsingDarkTheme();
			DarkModeCheckBox.IsChecked = darkMode;
		}

		public async void GoToSavedTagsPage(object sender, EventArgs args)
		{
			await Shell.Current.GoToAsync("savedtags");
		}

		public void OnToggle(object sender, CheckedChangedEventArgs args)
		{
			bool ret = ServiceHelper.GetService<DBService>().SetDarkTheme(args.Value);
			ThemeHelper.SetDarkTheme(args.Value);
		}
	}
}