using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace LoginPattern
{
	public class NewModel : ContentPage
	{
		string modelId, updateId;
		Dictionary<string,Object> models;
		RestService restService;
		Dictionary<string, Object> newModel;

		public NewModel()
		{
		}

		public NewModel(string id)
		{
			modelId = id;
			restService = new RestService();
			newModel = new Dictionary<string, Object>();
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			models = await restService.getModelInfo(modelId);

			var layout = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(5, 40, 5, 5),

			};
			var btnSend = new Button() { Text = "Send", HorizontalOptions = LayoutOptions.CenterAndExpand };
			btnSend.Clicked += BtnSend_Clicked;
			foreach( KeyValuePair<string, Object> kvp in models )
			{
				if (!(kvp.Key.ToString().Equals("id") || kvp.Key.ToString().Equals("createdAt") || kvp.Key.ToString().Equals("updatedAt")))
				{
					
					var values = JsonConvert.DeserializeObject<Dictionary<string, Object>>(kvp.Value.ToString());
					if (values != null)
					{
						layout.Children.Add(new Label() { Text = kvp.Key.ToString() });

						if (values.ContainsKey("x8bit"))
						{
							var x8bit = values["x8bit"];
							var detailValues = JsonConvert.DeserializeObject<Dictionary<string, Object>>(x8bit.ToString());
							var title = "";
							if (detailValues.ContainsKey("title"))
							{
								title = detailValues["title"].ToString();
							}

							if (detailValues.ContainsKey("column"))
							{
								var picker = new Picker();
								var columnData = JsonConvert.DeserializeObject<Dictionary<string, Object>>(detailValues["column"].ToString());
								if (columnData.ContainsKey("values"))
								{
									var valuesData = JsonConvert.DeserializeObject<Dictionary<string, string>>(columnData["values"].ToString());
									foreach (var item in valuesData)
										picker.Items.Add(item.Value.ToString());

								}
								layout.Children.Add(picker);
								newModel.Add(kvp.Key, picker);
							}
							else
							{
								var entry = new Entry() { Text = title };
								layout.Children.Add(entry);
								newModel.Add(kvp.Key, entry);
							}
						}

					}
				}
			}
			layout.Children.Add(btnSend);
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
				string dataValue="";
				if (item.Value.GetType() == typeof(Entry))
					dataValue = ((Entry)item.Value).Text;
				else if (item.Value.GetType() == typeof(Picker))
				{
					Picker pickerObj = (Picker)item.Value;
					if(pickerObj.Items[pickerObj.SelectedIndex]!=null)
						dataValue = pickerObj.Items[pickerObj.SelectedIndex];
				}
				jsonObject.Add(item.Key, dataValue);
			}
			var json = JsonConvert.SerializeObject(jsonObject);
			var result = await restService.postModels(modelId, json);
			if (result != null)
			{
				await DisplayAlert("Success", "The new record has been successfully created.", "OK");
				await Navigation.PopAsync(true);
			}
		}
	}
}

