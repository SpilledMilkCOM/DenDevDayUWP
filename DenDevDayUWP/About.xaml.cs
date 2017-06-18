using SM.Common.ViewModel;
using SM.DenDevDayUWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SM.DenDevDayUWP
{
	public sealed partial class About : Page
	{
		public About()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			ViewUtil.SetBackButtonVisibility();
		}

		private AboutViewModel ViewModel
		{
			get
			{
				return ViewUtil.ViewModel<AboutViewModel>(DataContext);
			}
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			ViewModel.Initialize(Frame);
		}
	}
}