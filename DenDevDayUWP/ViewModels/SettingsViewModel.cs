using SM.Common.Security;
using SM.Common.ViewModel;

namespace SM.DenDevDayUWP.ViewModels
{
	public class SettingsViewModel : ViewModelBase
	{
		private string _accessToken;
		private string _accessTokenSecret;
		private string _callbackUri;
		private string _consumerKey;
		private string _consumerKeySecret;

		public SettingsViewModel()
		{
			var oauth = new OAuth();

			oauth.Load();
		}

		public string AccessToken
		{
			get { return _accessToken; }
			set { SetValue(value, ref _accessToken); }
		}

		public string AccessTokenSecret
		{
			get { return _accessTokenSecret; }
			set { SetValue(value, ref _accessTokenSecret); }
		}

		public string CallbackUri
		{
			get { return _callbackUri; }
			set { SetValue(value, ref _callbackUri); }
		}

		public string ConsumerKey
		{
			get { return _consumerKey; }
			set { SetValue(value, ref _consumerKey); }
		}

		public string ConsumerKeySecret
		{
			get { return _consumerKeySecret; }
			set { SetValue(value, ref _consumerKeySecret); }
		}

		public override string PageTitle => "Settings";
	}
}