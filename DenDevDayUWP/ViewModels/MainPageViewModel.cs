using SM.Common.ViewModels;

namespace SM.DenDevDayUWP.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		public override string PageTitle => "PageTitlePlaceholder";

		//----==== PUBLIC ====---------------------------------------------------------------------

		public void ExecuteMenuItem(string label)
		{
			switch (label)
			{
				//case ABOUT_CMD:
				//	ViewUtil.ExecuteRelayCommand(AboutCommand);
				//	break;

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
	}
}