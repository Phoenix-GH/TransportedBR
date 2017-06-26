using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
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
			Navigation.PushAsync(new NewModel(id));
		}

		void OnLabelClicked(string itemId)
		{
			Navigation.PushAsync(new EditModel(modelId, itemId));
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
					new RowDefinition {Height = GridLength.Auto}
				}
			};

			int i = 0, j = 1;
			List<string> keyArray = new List<string>();
			foreach (var modelItem in models)
			{
				var model = JsonConvert.DeserializeObject<Dictionary<string, Object>>(modelItem.ToString());
				//grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
				foreach (var item in model.Keys)
				{
					
					if (keyArray.IndexOf(item) == -1)
					{
						keyArray.Add(item);
						grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
						grid.Children.Add(new Label() { Text = item.ToString(), BackgroundColor = Color.Gray, HorizontalOptions = LayoutOptions.FillAndExpand }, grid.ColumnDefinitions.Count - 1, 0);

					}
				}
			}

			foreach (var modelItem in models)
			{
				i = 0;

				var model = JsonConvert.DeserializeObject<Dictionary<string, Object>>(modelItem.ToString());

				foreach (var item in keyArray)
				{
					if (model.ContainsKey(item))
					{
						
						if (model[item] != null)
						{
							if (model[item].GetType() != typeof(Newtonsoft.Json.Linq.JObject))
							{
								Label label = new Label() { Text = model[item].ToString() };
								grid.Children.Add(label, i, j);
								label.GestureRecognizers.Add(new TapGestureRecognizer((View obj) => OnLabelClicked(model["id"].ToString())));

							}
						}

					}
					i++;
				}
				j++;

			}

			Content = new ScrollView
				{
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Padding = new Thickness(5, 40, 5, 5),
				Content = grid
			};
		}
	}						                              
}

