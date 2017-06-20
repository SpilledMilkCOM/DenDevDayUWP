using Microsoft.Toolkit.Uwp.Services.Twitter;
using SM.Common;
using SM.Common.ViewModel;
using System;
using System.Globalization;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SM.DenDevDayUWP.ViewModels
{
    public class SendTweetViewModel : ViewModelBase
    {
        private Visibility _busyVisibility;
        private bool _displayCoords;
        private string _latitude;
        private string _longitude;
        private string _message;
        private Brush _messageBrush;
        private Visibility _messageVisibility;
        private string _tweetText;
        private string _tweetTextHeader;

        private readonly Color _black = Color.FromArgb(0, 0, 0, 0);
        private readonly Color _green = Color.FromArgb(255, 0, 255, 0);
        private readonly Color _red = Color.FromArgb(255, 255, 0, 0);

        public SendTweetViewModel()
        {
            GetLocationCommand = new RelayCommand(GetLocation);
            TweetCommand = new RelayCommand(SendTweet);

            // Show this as a placeholder is designing.
            IsBusy = IsDesignModeEnabled;
            Message = (IsDesignModeEnabled) ? "Design mode." : string.Empty;

            // If TweetText changes then TweetTextHeader will be (property) notified.
            AddPropertyDependency(nameof(TweetTextHeader), nameof(TweetText));

            TweetText = string.Empty;
        }

        public bool DisplayCoords
        {
            get { return _displayCoords; }
            set { SetValue(value, ref _displayCoords); }
        }

        public string Latitude
        {
            get { return _latitude; }
            set { SetValue(value, ref _latitude); }
        }

        public string Longitude
        {
            get { return _longitude; }
            set { SetValue(value, ref _longitude); }
        }

        public string TweetText
        {
            get { return _tweetText; }
            set
            {
                SetValue(value, ref _tweetText);
            }
        }

        public string TweetTextHeader
        {
            get { return $"Tweet ({140 - TweetText.Length}/140)"; }
         }

        // TODO: Should put this stuff into some sort of control.  (Message & Progress)

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

        //----==== COMMANDS ====-------------------------------------------------------------------

        public RelayCommand GetLocationCommand { get; private set; }

        public RelayCommand TweetCommand { get; private set; }

        public override string PageTitle => "Tweet";

        //----==== PRIVATE ====--------------------------------------------------------------------

        private async void GetLocation()
        {
            try
            {
                var geolocator = new Geolocator();

                var position = await geolocator.GetGeopositionAsync();

                var pos = position.Coordinate.Point.Position;

                Latitude = pos.Latitude.ToString(CultureInfo.InvariantCulture);
                Longitude = pos.Longitude.ToString(CultureInfo.InvariantCulture);

                DisplayCoords = true;
            }
            catch (Exception ex)
            {
                await new MessageDialog($"An error occured finding your location. Message: {ex.Message}").ShowAsync();
            }
        }

        private async void SendTweet()
        {
            try
            {
                MessageBrush = new SolidColorBrush(_black);
                Message = "Sending tweet...";
                IsBusy = true;

                var status = new TwitterStatus
                {
                    DisplayCoordinates = DisplayCoords,
                    Message = TweetText,
                    Latitude = string.IsNullOrEmpty(Latitude) ? (double?)null : Convert.ToDouble(Latitude),
                    Longitude = string.IsNullOrEmpty(Longitude) ? (double?)null : Convert.ToDouble(Longitude)
                };

                await TwitterService.Instance.TweetStatusAsync(status);

                MessageBrush = new SolidColorBrush(_green);
                Message = "Success!";
            }
            catch (Exception ex)
            {
                await new MessageDialog($"An error occured sending your tweet. Message: {ex.Message}").ShowAsync();
                MessageBrush = new SolidColorBrush(_red);
                Message = "Error occurred.";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}