using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Windows.ApplicationModel;                 // For DesignMode
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using SM.Common.Models;

namespace SM.Common.ViewModel
{
	public abstract class ViewModelBase : ModelBase
	{
		private Frame _controllingPage;

		public ViewModelBase()
		{
			GoBackCommand = new RelayCommand(GoBack);
		}

		public string AppTitle { get { return AppConstants.AppTitleUpper; } }

		public Visibility BackButtonVisibility { get { return ToVisibility(ViewUtil.NeedsBackButton()); } }

		public Visibility DarkMaskVisibility { get { return ToVisibility(ViewUtil.IsThemeDark()); } }

		public Visibility LightMaskVisibility { get { return ToVisibility(!ViewUtil.IsThemeDark()); } }

		public RelayCommand GoBackCommand { get; private set; }

		public bool IsDesignModeEnabled {  get { return DesignMode.DesignModeEnabled; } }

		public abstract string PageTitle { get; }

		public virtual void Initialize(Frame controllingPage)
		{
			_controllingPage = controllingPage;
		}

		public void NavigateToRatings()
		{
			//var review = new MarketplaceReviewTask();

			//review.Show();
		}

		public void NavigateToUri(Uri uri)
		{
			var success = Launcher.LaunchUriAsync(uri);
		}

		public void NavigateToView(Type type, object parameter = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type", "type is null");
			}
			if (_controllingPage == null)
			{
				throw new ArgumentNullException("_controllingPage", "_controllingPage is null");
			}

			_controllingPage.Navigate(type, parameter);
		}

		public void NavigateToView(Uri view)
		{
			//if (_controllingFrame == null)
			//{
			//	throw new ArgumentNullException("_controllingFrame", "_controllingFrame is null");
			//}

			//_controllingFrame.Navigate(view);
		}

		protected void GoBack()
		{
			if (_controllingPage == null)
			{
				throw new ArgumentNullException("_controllingFrame", "_controllingFrame is null");
			}

			_controllingPage.GoBack();
		}

		protected bool IsVisible(Visibility visibility)
		{
			return visibility == Visibility.Visible;
		}

		protected ObservableCollection<T> ToObservableCollection<T>(List<T> list)
		{
			ObservableCollection<T> result = new ObservableCollection<T>();

			if (list != null)
			{
				foreach (T item in list)
				{
					result.Add(item);
				}
			}

			return result;
		}

		protected Visibility ToVisibility(bool isVisible)
		{
			return isVisible ? Visibility.Visible : Visibility.Collapsed;
		}
	}
}
