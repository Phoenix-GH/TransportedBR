using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace LoginPattern
{
	public class DetailPage : ContentPage
	{
		public DetailPage()
		{
			
			var logoImage = new Image();
			//if (App.config != null)
			//{
			//	if (App.config.logo != null)
			//	{
			//		if (App.config.logo.Count > 0)
			//			logoImage.Source = ImageSource.FromUri(new Uri(App.config.logo[0]));
			//	}
			//}
			Content = new StackLayout
			{
				HorizontalOptions = LayoutOptions.Center,
				Padding = new Thickness(10, 40, 10, 10),
				Children = {
					logoImage,
					new Label(){}
				}
			};
		}
	}
}

