using SM.Common;
using SM.Common.ViewModel;

namespace SM.DenDevDayUWP.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		public const string ABOUT_MENU_OPT = "About";
		public const string MY_TWEETS_MENU_OPT = "My Tweets";
		public const string PRIVACY_MENU_OPT = "Privacy";
		public const string SETTINGS_MENU_OPT = "Settings";

		public MainPageViewModel()
		{
			AboutCommand = new RelayCommand(About);
		}

		public override string PageTitle => "PageTitlePlaceholder";

		//----==== COMMANDS ====-------------------------------------------------------------------

		public RelayCommand AboutCommand { get; private set; }

		//----==== PUBLIC ====---------------------------------------------------------------------

		public void ExecuteMenuItem(string label)
		{
			switch (label)
			{
				case ABOUT_MENU_OPT:
					ViewUtil.ExecuteRelayCommand(AboutCommand);
					break;

					//case MY_TWEETS_CMD:
					//	ViewUtil.ExecuteRelayCommand(MyTweetsCommand);
					//	break;

					//case PRIVACY_CMD:
					//	ViewUtil.ExecuteRelayCommand(PrivacyCommand);
					//	break;

					//case SETTINGS_CMD:
					//	ViewUtil.ExecuteRelayCommand(SettingsCommand);
					//	break;
			}
		}

		//----==== PRIVATE ====--------------------------------------------------------------------

		private void About()
		{
			NavigateToView(typeof(About));
		}
	}
}