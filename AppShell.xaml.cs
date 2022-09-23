using Ergie.Helper;
using Ergie.Pages;
using Ergie.Services;
using Ergie.Views;

namespace Ergie;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("pickcolor", typeof(ColorPicker));
		Routing.RegisterRoute("savedtags", typeof(SavedTagsPage));
		Routing.RegisterRoute("viewitem", typeof(ViewItemPage));
	}
}
