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
		public MenuPage ()
		{
			Title = "LoginPattern";
			Icon = "slideout.png";

			section = new TableSection () {
				new TextCell {Text = "Sessions"},
				new TextCell {Text = "Speakers"},
				new TextCell {Text = "Favorites"},
				new TextCell {Text = "Room Plan"},
				new TextCell {Text = "Map"},
			};

			var root = new TableRoot () {section} ;

			tableView = new TableView ()
			{ 
				Root = root,
				Intent = TableIntent.Menu,
			};

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

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			RestService service = new RestService();
			var menuItems = await service.getMenu();
			//section.Clear();
			if (menuItems != null)
			{
				foreach (var item in menuItems)
				{
					section.Add(new TextCell { Text = item.name });
				}
			}
		}
	}
}

