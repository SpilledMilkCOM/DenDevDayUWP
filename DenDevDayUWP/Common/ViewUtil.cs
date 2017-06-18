using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using SM.Common;
using Windows.UI.Core;

namespace SM.Common.ViewModel
{
    public static class ViewUtil
	{
		public static void ExecuteRelayCommand(RelayCommand command)
		{
			if (command.CanExecute(null))
			{
				command.Execute(null);
			}
		}

		public static bool IsThemeDark()
		{
			return Application.Current.RequestedTheme == ApplicationTheme.Dark || DesignMode.DesignModeEnabled;
		}

		internal static bool NeedsBackButton()
		{
			return true;
		}

        public static void SetBackButtonVisibility()
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }

		public static TViewModel ViewModel<TViewModel>(object dataContext)
			where TViewModel : class
		{

			TViewModel viewModel = dataContext as TViewModel;

			if (viewModel == null)
			{
				throw new ArgumentNullException("dataContext", string.Format("The DataContext was not initialized with the {0}", typeof(TViewModel).Name));
			}

			return viewModel;
		}
	}
}