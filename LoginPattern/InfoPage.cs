using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace LoginPattern
{
	public class InfoPage : ContentPage
	{
		Grid grid;
		string modelId;
		public InfoPage()
		{
		}

		public InfoPage(string id)
		{
			modelId = id;
			ToolbarItem btnAdd = new ToolbarItem
			{
				Text = "Add",
				Order = ToolbarItemOrder.Primary,
				Command = new Command(() => showDetailPage(id))
			};
			ToolbarItems.Add(btnAdd);

		}
		void showDetailPage(string id)
		{
			Navigation.PushAsync(new QuestionPage(id));
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			var restService = new RestService();
			var models = await restService.getModels(modelId);
			grid = new Grid()
			{
				HorizontalOptions = LayoutOptions.Fill,
				RowDefinitions = {
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
				}
			};

			int i = 0;
			foreach (var item in models.Keys)
			{
				grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
				grid.Children.Add(new Label(){Text = item.ToString(), BackgroundColor = Color.Gray, HorizontalOptions = LayoutOptions.FillAndExpand},i,0);
				i++;
			}
			i = 0;
			foreach (var item in models.Values)
			{
				if (item != null)
				{
					if (item.GetType() != typeof(Newtonsoft.Json.Linq.JObject))
					{
						grid.Children.Add(new Label() { Text = item.ToString() }, i, 1);
						Debug.WriteLine(item.GetType().ToString());
					}
				}
				i++;
			}
			FloatingActionButton floatingActionButton = new FloatingActionButton()
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				WidthRequest = 50,
				HeightRequest = 50,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Image = "ic_add_white.png",
				ButtonColor = Color.FromHex("03A9F4")
			};
			floatingActionButton.Clicked += FloatingActionButton_Clicked;
			Content = new StackLayout
			{
				HorizontalOptions = LayoutOptions.Center,
				Padding = new Thickness(5, 40, 5, 5),
				Children = {
					grid,
					//floatingActionButton
				}
			};
		}

		void FloatingActionButton_Clicked(object sender, EventArgs e)
		{

		}
	}
}

