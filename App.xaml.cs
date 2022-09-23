using Ergie.ViewModels;
using Ergie.Services;
using Ergie.Helper;

namespace Ergie;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new AppShell();
	}
}
