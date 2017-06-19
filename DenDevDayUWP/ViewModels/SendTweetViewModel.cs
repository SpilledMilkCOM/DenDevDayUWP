using Microsoft.Toolkit.Uwp.Services.Twitter;
using SM.Common;
using SM.Common.ViewModel;
using System;
using System.Globalization;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;

namespace SM.DenDevDayUWP.ViewModels
{
    public class SendTweetViewModel : ViewModelBase
    {
        private bool _displayCoords;
        private string _latitude;
        private string _longitude;
        private string _tweetText;

        public SendTweetViewModel()
        {
            GetLocationCommand = new RelayCommand(GetLocation);
            TweetCommand = new RelayCommand(SendTweet);
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
            set { SetValue(value, ref _tweetText); }
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
                var status = new TwitterStatus
                {
                    DisplayCoordinates = DisplayCoords,
                    Message = TweetText,
                    Latitude = string.IsNullOrEmpty(Latitude) ? (double?)null : Convert.ToDouble(Latitude),
                    Longitude = string.IsNullOrEmpty(Longitude) ? (double?)null : Convert.ToDouble(Longitude)
                };

                await TwitterService.Instance.TweetStatusAsync(status);
            }
            catch (Exception ex)
            {
                await new MessageDialog($"An error occured sending your tweet. Message: {ex.Message}").ShowAsync();
            }
        }
    }
}