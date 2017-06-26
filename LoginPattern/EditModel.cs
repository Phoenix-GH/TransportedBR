using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace LoginPattern
{
	public class EditModel : ContentPage
	{
		string modelId,itemId;
		Dictionary<string,Object> model;
		RestService restService;
		Dictionary<string, Object> newModel;

		public EditModel()
		{
		}

		public EditModel(string id, string itemId="0")
		{
			modelId = id;
			restService = new RestService();
			newModel = new Dictionary<string, Object>();
			this.itemId = itemId;
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			model = await restService.getModel(modelId,itemId);

			var layout = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(5, 40, 5, 5),

			};
			var btnSend = new Button() { Text = "Update", HorizontalOptions = LayoutOptions.CenterAndExpand };
			btnSend.Clicked += BtnSend_Clicked;
			var btnDelete = new Button() { Text = "Delete", HorizontalOptions = LayoutOptions.CenterAndExpand };
			btnDelete.Clicked += BtnDelete_Clicked;
			foreach( KeyValuePair<string, Object> kvp in model )
			{
				
				if (!(kvp.Key.ToString().Equals("id") || kvp.Key.ToString().Equals("createdAt") || kvp.Key.ToString().Equals("updatedAt")))
				{
					
					if (kvp.Value != null)
					{
						if (kvp.Value.GetType() != typeof(Newtonsoft.Json.Linq.JObject))
						{
							layout.Children.Add(new Label() { Text = kvp.Key.ToString() });
							var entry = new Entry() { Text = kvp.Value.ToString() };
							layout.Children.Add(entry);
							newModel.Add(kvp.Key, entry);
						}
					}
				}
			}
			layout.Children.Add(btnSend);
			layout.Children.Add(btnDelete);
			Content = new ScrollView() 
			{ 
				Content = layout 
			};
		}

		async void BtnSend_Clicked(object sender, EventArgs e)
		{
			var jsonObject = new Dictionary<string, string>();
			foreach (var item in newModel)
			{
				string dataValue = "";
				if (item.Value.GetType() == typeof(Entry))
					dataValue = ((Entry)item.Value).Text;
				
				jsonObject.Add(item.Key, dataValue);
			}
			var json = JsonConvert.SerializeObject(jsonObject);

			var result = await restService.updateModels(modelId, json, itemId);
			if (result != null)
			{
				await DisplayAlert("Success", "The record has been successfully updated.", "OK");
				await Navigation.PopAsync(true);
			}
			
		}

		async void BtnDelete_Clicked(object sender, EventArgs e)
		{
			
			var result = await restService.deleteModel(modelId, itemId);
			if (result != null)
			{
				await DisplayAlert("Success", "The record has been successfully deleted.", "OK");
				await Navigation.PopAsync(true);
			}
		}
	}
}

