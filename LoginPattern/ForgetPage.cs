using System;
using Xamarin.Forms;

namespace LoginPattern
{
	public class ForgetPage : ContentPage
	{
		public ForgetPage (ILoginManager ilm)
		{
			var button = new Button { Text = "Forgot Password" };
			button.Clicked += (sender, e) => {
				DisplayAlert("Account created", "Add processing login here", "Send");
				ilm.ShowMainPage();
			};
			var cancel = new Button { Text = "Login" };
			cancel.Clicked += (sender, e) => {
				MessagingCenter.Send<ContentPage> (this, "Login");
			};
			Content = new StackLayout {
				Padding = new Thickness (10, 40, 10, 10),
				Children = {
					new Label { Text = "Forgot Password", Font = Font.SystemFontOfSize(NamedSize.Large) }, 
					new Label { Text = "Choose a Username" },
					new Entry { Text = "" },
					
					button, cancel
				}
			};
		}
	}
}

