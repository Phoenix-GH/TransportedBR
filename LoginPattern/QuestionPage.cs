using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace LoginPattern
{
	public class QuestionPage : ContentPage
	{
		string modelId;
		Dictionary<string,Object> models;
		RestService restService; 
		public QuestionPage()
		{
		}

		public QuestionPage(string id)
		{
			modelId = id;
			restService = new RestService();
		}


		protected async override void OnAppearing()
		{
			base.OnAppearing();
			models = await restService.getModelInfo(modelId);
			//Debug.WriteLine(models.Keys.ToString());
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
					layout.Children.Add(new Label() { Text = kvp.Key.ToString() });
					var values = JsonConvert.DeserializeObject<Dictionary<string, Object>>(kvp.Value.ToString());
					if (values != null)
					{
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
								layout.Children.Add(new Picker());
								var columnData = JsonConvert.DeserializeObject<Dictionary<string, Object>>(detailValues["column"].ToString());
								if (columnData.ContainsKey("values"))
								{
									var valuesData = JsonConvert.DeserializeObject<Dictionary<string, string>>(columnData["values"].ToString());
									foreach (var item in valuesData)
									{
										picker.Items.Add(item.Value.ToString());
									}
								}
							}
							else
							{
								layout.Children.Add(new Entry() { Text = title });
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
			Debug.WriteLine(models.Keys.ToString());
			await restService.postModels(modelId, models);
		}
	}
}

