using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoginPattern
{
	public class MenuPage : ContentPage
	{
		MasterDetailPage master;

		TableView tableView;
		TableSection section;

		public MenuPage()
		{
			
		}
		public MenuPage (MasterDetailPage parent)
		{
			Title = "LoginPattern";
			Icon = "slideout.png";
			master = parent;
			section = new TableSection () {
			};

			var root = new TableRoot () {section} ;

			tableView = new TableView ()
			{ 
				Root = root,
				Intent = TableIntent.Menu,
			};
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			RestService service = new RestService();
			var menuItems = await service.getMenu();

			if (menuItems != null)
			{
				foreach (var item in menuItems)
				{
					section.Add(new TextCell { Text = item.name, Command = new Command(() => openDetails(item.model))});
				}
			}
			var logoutButton = new Button { Text = "Logout" };
			logoutButton.Clicked += (sender, e) => {
				App.Current.Logout();
			};
			Content = new StackLayout {
				BackgroundColor = Color.Gray,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					tableView, 
					logoutButton
				}
			};
		}

		void openDetails(string id)
		{
			if (master != null)
			{
				Navigation.PushAsync(new InfoPage(id));
			}
		}
	}
}

