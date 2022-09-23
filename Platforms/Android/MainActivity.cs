using Android.Runtime;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Ergie.Helper;

[assembly: Dependency(typeof(Ergie.MainActivity.Environment))]
namespace Ergie;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
	public class Environment : IEnvironment
	{
		public void SetStatusBarColor(Color color, bool dark)
		{
			var a = (int)Math.Round(color.Alpha * 255.0);
			var r = (int)Math.Round(color.Red * 255.0);
			var g = (int)Math.Round(color.Green * 255.0);
			var b = (int)Math.Round(color.Blue * 255.0);

			if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
				return;

			var activity = Platform.CurrentActivity;
			var window = activity.Window;
			window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
			window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
			window.SetStatusBarColor(Android.Graphics.Color.Argb(a, r, g, b));

			if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
			{
				var flag = (Android.Views.StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
#pragma warning disable CS0618 // Type or member is obsolete
				window.DecorView.SystemUiVisibility = dark ? 0 : flag;
#pragma warning restore CS0618 // Type or member is obsolete
			}
		}
	}
}
