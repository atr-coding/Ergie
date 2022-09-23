using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ergie.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		bool isBusy;

		public bool IsBusy
		{
			get => isBusy;
			set
			{
				if(isBusy == value)
				{
					return;
				}
				isBusy = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string name = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
