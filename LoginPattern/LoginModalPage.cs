using System;
using Xamarin.Forms;

namespace LoginPattern
{
	public class LoginModalPage : CarouselPage
	{
        ContentPage login, create, forget;
		public LoginModalPage (ILoginManager ilm)
		{
			login = new LoginPage (ilm);
			create = new CreateAccountPage (ilm);
            forget = new ForgetPage(ilm);
			this.Children.Add (login);
			this.Children.Add (create);
            this.Children.Add(forget);
			MessagingCenter.Subscribe<ContentPage> (this, "Login", (sender) => {
				this.SelectedItem = login;
			});
			MessagingCenter.Subscribe<ContentPage> (this, "Create", (sender) => {
				this.SelectedItem = create;
			});

			MessagingCenter.Subscribe<ContentPage>(this, "Forget", (sender) =>
			{
                this.SelectedItem = forget;
			});
		}
	}
}

