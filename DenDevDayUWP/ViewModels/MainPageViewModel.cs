using Microsoft.Toolkit.Uwp.Services.Twitter;
using SM.Common;
using SM.Common.Security;
using SM.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SM.DenDevDayUWP.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		public const string ABOUT_MENU_OPT = "About";
		public const string LOGOUT_MENU_OPT = "Logout";
		public const string TWEET_MENU_OPT = "Tweet";
		public const string PRIVACY_MENU_OPT = "Privacy";
		public const string SETTINGS_MENU_OPT = "Settings";

		private const string ATTAG = "@";

		private Visibility _busyVisibility;
		private string _message;
		private Brush _messageBrush;
		private Visibility _messageVisibility;
		private ObservableCollection<Tweet> _tweets;
		private Uri _userImage;
		private string _userScreenName;

		public MainPageViewModel()
		{
			_tweets = new ObservableCollection<Tweet>();

			AboutCommand = new RelayCommand(About);
			LogoutCommand = new RelayCommand(Logout);
			SettingsCommand = new RelayCommand(Settings);
            SendTweetCommand = new RelayCommand(SendTweet);

            if (!IsDesignModeEnabled)
			{
				InitializeTwitter();
			}
		}

		public Visibility BusyVisibility
		{
			get { return _busyVisibility; }
			set { SetValue(value, ref _busyVisibility); }
		}

		private bool IsBusy
		{
			get { return IsVisible(BusyVisibility); }
			set { BusyVisibility = ToVisibility(value); }
		}

		public string Message
		{
			get
			{
				return _message;
			}

			set
			{
				SetValue(value, ref _message);
			}
		}

		public Brush MessageBrush
		{
			get { return _messageBrush; }
			set
			{
				SetValue(value, ref _messageBrush);
			}
		}

		public Visibility MessageVisibility
		{
			get { return _messageVisibility; }
			set { SetValue(value, ref _messageVisibility); }
		}

		public override string PageTitle => "Mah Tweets";

		public Uri UserImage
		{
			get
			{
				return _userImage;
			}

			set
			{
				SetValue(value, ref _userImage);
			}
		}

		public string UserScreenName
		{
			get { return _userScreenName; }
			set { SetValue(value, ref _userScreenName); }
		}

		public ObservableCollection<Tweet> Tweets
		{
			get { return _tweets; }
			set { SetValue(value, ref _tweets); }
		}

		//----==== COMMANDS ====-------------------------------------------------------------------

		public RelayCommand AboutCommand { get; private set; }

		public RelayCommand LogoutCommand { get; private set; }

		public RelayCommand SettingsCommand { get; private set; }

        public RelayCommand SendTweetCommand { get; private set; }

        //----==== PUBLIC ====---------------------------------------------------------------------

        public void ExecuteMenuItem(string label)
		{
			switch (label)
			{
				case ABOUT_MENU_OPT:
					ViewUtil.ExecuteRelayCommand(AboutCommand);
					break;

				case LOGOUT_MENU_OPT:
					ViewUtil.ExecuteRelayCommand(LogoutCommand);
					break;

				case SETTINGS_MENU_OPT:
					ViewUtil.ExecuteRelayCommand(SettingsCommand);
					break;

                case TWEET_MENU_OPT:
                    ViewUtil.ExecuteRelayCommand(SendTweetCommand);
                    break;

                    //case PRIVACY_CMD:
                    //	ViewUtil.ExecuteRelayCommand(PrivacyCommand);
                    //	break;

            }
        }

		//----==== PRIVATE ====--------------------------------------------------------------------

		private void About()
		{
			NavigateToView(typeof(About));
		}

		private async void FetchTweets(string fetchText)
		{
			IEnumerable<Tweet> timeLine = null;

			try
			{
				IsBusy = true;

				var filter = GetAccount(fetchText);

				if (filter != null)
				{
					timeLine = await TwitterService.Instance.GetUserTimeLineAsync(filter, 10);
				}
				else
				{
					timeLine = await TwitterService.Instance.SearchAsync(fetchText, 10);
				}

				Tweets.Clear();

				foreach (var tweet in timeLine)
				{
					Tweets.Add(tweet);
				}
			}
			finally
			{
				IsBusy = false;
			}

		}

		private string GetAccount(string account)
		{
			string result = null;

			if (account != null && account.IndexOf(ATTAG) == 0)
			{
				result = account.Replace(ATTAG, string.Empty);
			}

			return result;
		}

		private async void InitializeTwitter()
		{
			try
			{
				IsBusy = true;

				Message = "Logging into Twitter...";

				var oauth = new OAuth();

				oauth.Load();

				TwitterService.Instance.Initialize(oauth.ConsumerKey, oauth.ConsumerKeySecret, oauth.CallbackUri);

				var user = await TwitterService.Instance.GetUserAsync();

				UserScreenName = user.ScreenName;
				UserImage = new Uri(user.ProfileImageUrl);

				Message = $"Logged in as {user.ScreenName}";

				FetchTweets($"@{user.ScreenName}");
			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private void Logout()
		{
			TwitterService.Instance.Logout();

			Application.Current.Exit();
		}

        private void SendTweet()
        {
            NavigateToView(typeof(SendTweet));
        }

        private void Settings()
		{
			NavigateToView(typeof(Settings));
		}

		private void ShowMessage(string message)
		{
			//if (_dispatcher != null)
			//{
			//	_dispatcher.BeginInvoke(() => MessageBox.Show(message));
			//}
		}
	}
}