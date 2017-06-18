using Microsoft.Toolkit.Uwp.UI.Controls;
using SM.Common.ViewModel;
using SM.DenDevDayUWP.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DenDevDayUWP
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

		private MainPageViewModel ViewModel
		{
			get
			{
				return ViewUtil.ViewModel<MainPageViewModel>(DataContext);
			}
		}

		private void HandleMenuClick(object sender, ItemClickEventArgs e)
		{
			var item = e.ClickedItem as HamburgerMenuGlyphItem;

			if (item != null)
			{
				ViewModel.ExecuteMenuItem(item.Label);
			}
		}
	}
}