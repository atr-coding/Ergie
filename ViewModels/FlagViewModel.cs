using Ergie.Models;
using Ergie.Services;
using System.Collections.ObjectModel;

namespace Ergie.ViewModels
{
	public class FlagViewModel : BaseViewModel
	{
		public DBService DBService { get; }
		public ObservableCollection<Flag> Flags { get; set; } = null;

		public FlagViewModel(DBService dbService)
		{
			DBService = dbService;
		}

		public void CreateSavedFlag(Flag flag)
		{
			DBService.AddSavedFlag(flag);
			Flags.Add(flag);
		}

		public void DeleteSavedFlag(Flag flag)
		{
			DBService.DeleteSavedFlag(flag);
			Flags.Remove(flag);
		}
		
		public void ReloadFlags()
		{
			Flags.Clear();
			Flags = null;
			LoadSavedFlags();
		}

		public void LoadSavedFlags()
		{
			if(Flags != null)
			{
				return;
			}

			var flags = DBService.GetSavedFlags();

			Flags = new();

			if (flags != null && flags.Count > 0)
			{
				foreach(var flag in flags)
				{
					Flags.Add(flag);
				}
			}
		}
	}
}
