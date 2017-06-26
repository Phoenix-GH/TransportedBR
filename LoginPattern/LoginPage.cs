using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LoginPattern
{
	public class LoginPage : ContentPage
	{
		RestService service;
		Entry username, password;
		Image logoImage;
		public LoginPage (ILoginManager ilm)
		{
			var button = new Button { Text = "Login" };
			service = new RestService();
			logoImage = new Image();
			button.Clicked += async (sender, e) => {
				if (String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Text))
				{
					await DisplayAlert("Validation Error", "Username and Password are required", "Re-try");
				} else{
                    
                    Login user = new Login();
                    user.email = username.Text;
                    user.password = password.Text;
                    User result = await service.Login(user);
					if (result != null)
					{
						App.Current.Properties["IsLoggedIn"] = true;
						App.user = result;

						ilm.ShowMainPage();
					}
					else
					{
						await DisplayAlert("Login failed", "Invalid credentials", "Re-try");
					}
				}
			};
			var create = new Button { Text = "Create Account" ,BackgroundColor=Color.Transparent};
			create.Clicked += (sender, e) => {
				MessagingCenter.Send<ContentPage> (this, "Create");
			};

			var forget = new Button { Text = "Forgot Password",BackgroundColor=Color.Transparent};
			forget.Clicked += (sender, e) =>
			{
				MessagingCenter.Send<ContentPage>(this, "Forget");
			};

			username = new Entry { Text = "admin@x8bit.com" };
			password = new Entry { Text = "asdqwe123", IsPassword = true};
			Content = new StackLayout {
				Padding = new Thickness (10, 40, 10, 10),
				Children = {
					new Image(){},
					new Label { Text = "Login", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 
					new Label { Text = "Username" },
					username,
					new Label { Text = "Password" },
					password,
					button, create, forget
				}
			};
		}
		protected async override void OnAppearing()
		{
			base.OnAppearing();
			//var configs = await service.getConfig();

			//if (configs != null)
			//{
			//	foreach (var item in configs)
			//	{
			//		if (item != null)
			//		{
			//			App.config = item;
			//			break;
			//		}
			//	}
			//}

			//if (App.config.logo != null)
			//{
			//	if(App.config.logo.Count > 0)
			//		logoImage.Source = ImageSource.FromUri(new Uri(App.config.logo[0]));
			//}
		}
	}
}

