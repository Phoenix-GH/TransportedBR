using System;
using Xamarin.Forms;

namespace LoginPattern
{
	public class DetailPage : ContentPage
	{
		public DetailPage ()
		{
			
			Content = new StackLayout { 
				HorizontalOptions = LayoutOptions.Center,
				Padding = new Thickness (10, 40, 10, 10),
				Children = {
					
				}
			};
		}
	}
}

