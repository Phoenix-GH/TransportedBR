using System;
using Xamarin.Forms;

namespace LoginPattern
{
	public class LoginPage : ContentPage
	{
		Entry username, password;
		public LoginPage (ILoginManager ilm)
		{
			var button = new Button { Text = "Login" };
			button.Clicked += async (sender, e) => {
				if (String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Text))
				{
					await DisplayAlert("Validation Error", "Username and Password are required", "Re-try");
				} else {
                    // REMEMBER LOGIN STATUS!
                    RestService service = new RestService();
                    Login user = new Login();
                    user.email = username.Text;
                    user.password = password.Text;
                    User result = await service.Login(user);
					if (result != null)
					{
						App.Current.Properties["IsLoggedIn"] = true;
						ilm.ShowMainPage();
					}
					else
					{
						await DisplayAlert("Login failed", "Invalid credentials", "Re-try");
					}
				}
			};
			var create = new Button { Text = "Create Account" };
			create.Clicked += (sender, e) => {
				MessagingCenter.Send<ContentPage> (this, "Create");
			};

			var forget = new Button { Text = "Forgot Password" };
			forget.Clicked += (sender, e) =>
			{
				MessagingCenter.Send<ContentPage>(this, "Forget");
			};

			username = new Entry { Text = "" };
			password = new Entry { Text = "" };
			Content = new StackLayout {
				Padding = new Thickness (10, 40, 10, 10),
				Children = {
					new Label { Text = "Login", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 
					new Label { Text = "Username" },
					username,
					new Label { Text = "Password" },
					password,
					button, create, forget
				}
			};
		}
	}
}

