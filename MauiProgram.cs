using Ergie.Database;
using Ergie.ViewModels;
using Ergie.Services;

namespace Ergie;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<DBConnection>();
		builder.Services.AddSingleton<DBService>();
		builder.Services.AddSingleton<ListViewModel>();
		builder.Services.AddSingleton<ItemViewModel>();
		builder.Services.AddSingleton<FlagViewModel>();
#if ANDROID
		builder.Services.AddTransient<MainActivity.Environment>();
#endif
		return builder.Build();
	}
}
