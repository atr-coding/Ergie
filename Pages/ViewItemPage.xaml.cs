using Ergie.Helper;
using Ergie.Models;
using Ergie.ViewModels;

namespace Ergie.Pages
{
	public partial class ViewItemPage : ContentPage, IQueryAttributable
	{
		public FlagViewModel FlagViewModel { get; set; }
		ItemWithFlags item;

		public ViewItemPage()
		{
			InitializeComponent();
			FlagViewModel = ServiceHelper.GetService<FlagViewModel>();
		}

		public void ApplyQueryAttributes(IDictionary<string, object> query)
		{
			if (query.ContainsKey("item"))
			{
				item = query["item"] as ItemWithFlags;
				ItemTitle.Text = item.Item.Title;
				ItemDescription.Text = item.Item.Description;
				ItemFlagCollectionView.ItemsSource = item.Flags;
			}
		}

		//public void DeleteFlag(object sender, EventArgs args)
		//{
		//	var flag = (sender as Button).CommandParameter as ItemFlag;
		//	FlagViewModel.DBService.DeleteItemFlag(flag);
		//	item.Flags.Remove(flag);
		//	OnPropertyChanged(nameof(ItemFlagCollectionView.ItemsSource));
		//}
	}
}