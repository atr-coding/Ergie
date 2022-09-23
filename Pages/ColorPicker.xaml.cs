using System.Diagnostics;

namespace Ergie;

public partial class ColorPicker : ContentPage
{
	public ColorPicker()
	{
		InitializeComponent();
	}

	void SliderChange(object sender, EventArgs args)
	{
		double red = RedSlider.Value / 255.0;
		double green = GreenSlider.Value / 255.0;
		double blue = BlueSlider.Value / 255.0;

		RedValueLabel.Text = Math.Round(RedSlider.Value).ToString();
		GreenValueLabel.Text = Math.Round(GreenSlider.Value).ToString();
		BlueValueLabel.Text = Math.Round(BlueSlider.Value).ToString();

		ColorPreview.BackgroundColor = Color.FromRgb(red, green, blue);
	}

	async void SelectColor(object sender, EventArgs args)
	{
		double red = RedSlider.Value / 255.0;
		double green = GreenSlider.Value / 255.0;
		double blue = BlueSlider.Value / 255.0;
		var color = Color.FromRgb(red, green, blue).ToHex()[1..];
		await Shell.Current.GoToAsync($"..?selectedColor={color}");
	}
}