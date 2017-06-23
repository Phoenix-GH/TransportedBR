using System;

using Xamarin.Forms;

namespace LoginPattern
{
	public class InfoPage : ContentPage
	{
		ListView list;
		string modelId;
		public InfoPage()
		{
		}

		public InfoPage(string id)
		{
			list = new ListView();
			modelId = id;
			list.ItemSelected += List_ItemSelected;
			BackgroundColor = new Color(0, 0, 1, 0.2);

		}
		protected async override void OnAppearing()
		{
			base.OnAppearing();
			var restService = new RestService();
			var models = await restService.getModels(modelId);
			list.ItemsSource = models;
			Content = new StackLayout
			{
				HorizontalOptions = LayoutOptions.Center,
				Padding = new Thickness(10, 40, 10, 10),
				Children = {
					list
				}
			};
		}

		void List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{

		}
	}
}

