using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LoginPattern
{
	public class RestService : IRestService
	{
		HttpClient client;

        public List<Model> Items { get; private set; }
		public List<Menu> menuItems { get; private set; }
		public RestService ()
		{
			//var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
			//var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

			client = new HttpClient ();
			client.BaseAddress = new Uri(Constants.RestUrl);
			client.MaxResponseContentBufferSize = 2560000;
		}

		public async Task<User> Login(Login user)
		{
			Items = new List<Model>();
			var uri = new Uri(Constants.RestUrl+"user/login");
			try
			{
				var json = JsonConvert.SerializeObject(user);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				HttpResponseMessage response = null;
				response = await client.PostAsync(uri, content);		
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync ();
					Debug.WriteLine(result);
					App.user = JsonConvert.DeserializeObject<User>(result);
					return App.user;
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"             ERROR {0}", ex.Message);
			}
			return null;
		}

		public async Task<IEnumerable<Menu>> getMenu()
		{
			client.DefaultRequestHeaders.Add("Authorization", App.user.tokenLogin);
			menuItems = new List<Menu>();
			var uri = new Uri(Constants.RestUrl + "menu");

			try {
				var response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode) {
					var content = await response.Content.ReadAsStringAsync();
					menuItems = JsonConvert.DeserializeObject<List<Menu>> (content);
					Debug.WriteLine(menuItems);
				}

			} catch (Exception ex) {
				Debug.WriteLine (@"				ERROR {0}", ex.Message);
			}
			return menuItems;
		}

		public async Task<Dictionary<string,Object>> getModels(string model)
		{
			client.DefaultRequestHeaders.Add("Authorization", App.user.tokenLogin);
			var models = new Dictionary<string, Object>();
			var uri = new Uri(Constants.RestUrl + model);

			try {
				var response = await client.GetAsync(uri);

				if (response.IsSuccessStatusCode) {
					var content = await response.Content.ReadAsStringAsync();

					var objects = JsonConvert.DeserializeObject<List<Object>> (content);
					models = JsonConvert.DeserializeObject<Dictionary<string, Object>> (objects[0].ToString());
					Debug.WriteLine(models);
				}

			} catch (Exception ex) {
				Debug.WriteLine (@"				ERROR {0}", ex.Message);
			}
			return models;
		}

		public async Task<Dictionary<string, Object>> getModelInfo(string model)
		{
			client.DefaultRequestHeaders.Add("Authorization", App.user.tokenLogin);
			var models = new Dictionary<string, Object>();
			var uri = new Uri(Constants.RestUrl + "base/modelinfo?model=" + model);

			try
			{
				var response = await client.GetAsync(uri);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					models = JsonConvert.DeserializeObject<Dictionary<string, Object>>(content);
					Debug.WriteLine(models);
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return models;
		}

		public async Task<Dictionary<string, Object>> postModels(string model, Object postData)
		{
			client.DefaultRequestHeaders.Add("Authorization", App.user.tokenLogin);

			var uri = new Uri(Constants.RestUrl + model);
			//Debug.WriteLine(postData.Keys.ToString());
			//try
			//{
			//	var json = JsonConvert.SerializeObject(postData);
			//	var content = new StringContent(json, Encoding.UTF8, "application/json");
			//	HttpResponseMessage response = null;

			//	response = await client.PostAsync(uri, content);		
			//	if (response.IsSuccessStatusCode)
			//	{
			//		var result = await response.Content.ReadAsStringAsync();
			//		Debug.WriteLine(result);
			//		Dictionary<string, Object> formattedResult = JsonConvert.DeserializeObject<Dictionary<string,Object>>(result);
			//		return formattedResult;
			//	}

			//}
			//catch (Exception ex)
			//{
			//	Debug.WriteLine(@"             ERROR {0}", ex.Message);
			//}
			return null;
			}

	}
}
