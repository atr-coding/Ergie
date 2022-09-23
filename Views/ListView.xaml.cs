using Ergie.Helper;
using Ergie.Models;
using Ergie.ViewModels;
using System.Diagnostics;

namespace Ergie.Views
{
	public partial class ListView : ContentPage
	{
		public ListViewModel ListViewModel { get; }

		public ListView()
		{
			InitializeComponent();
			ListViewModel = ServiceHelper.GetService<ListViewModel>();
			ListViewModel.LoadTodoLists();
			BindingContext = ListViewModel;
		}

		private async void SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var list = e.CurrentSelection.FirstOrDefault() as TodoList;
			if (list != null)
			{
				ListCollectionView.SelectedItem = null;
				await Shell.Current.GoToAsync($"//Home?listId={list.Id}");
			}
		}

		public void ToggleDelete(object sender, EventArgs args)
		{
			ListViewModel.ShowDeleteButtons = !ListViewModel.ShowDeleteButtons;
			ListViewModel.OnPropertyChanged(nameof(ListViewModel.ShowDeleteButtons));
		}

		public async void CreateList(object sender, EventArgs args)
		{
			string result = await DisplayPromptAsync("Create List", "Title:");
			
			while (result.Length > 50)
			{
				result = await DisplayPromptAsync("Create List", $"List titles have a maximum length of 50 characters.You're title had {result.Length} characters.", initialValue: result);
				if(result == null)
				{
					return;
				}
			}

			ListViewModel.AddTodoList(new TodoList
			{
				Name = result
			});
		}
	}
}