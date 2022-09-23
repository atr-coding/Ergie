using Ergie.Services;
using Ergie.Themes;

namespace Ergie.Helper
{
	public static class ThemeHelper
	{
		public static void SetDarkTheme(bool value)
		{
			ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
			if (mergedDictionaries != null)
			{
				mergedDictionaries.Clear();
				if (value)
				{
					mergedDictionaries.Add(new DarkTheme());
				}
				else
				{
					mergedDictionaries.Add(new LightTheme());
				}
#if ANDROID
				ServiceHelper.GetService<MainActivity.Environment>().SetStatusBarColor((Color)mergedDictionaries.First()["BackgroundColor"], value);
#endif
			}
		}
	}
}
