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

			//client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "http://developer.github.com/v3/#user-agent-required");
string _ContentType = "application/json";
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));
			client.DefaultRequestHeaders.Add("Access-Control-Allow-Methods", "GET");
			client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "*");
			//client.DefaultRequestHeaders.Add("Accept", "application/*+xml;version=5.1");
			
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
		}

		public async Task<List<Model>> RefreshDataAsync ()
		{
			Items = new List<Model> ();
			var uri = new Uri (string.Format (Constants.RestUrl, string.Empty));

			try {
				var response = await client.GetAsync (uri);
				if (response.IsSuccessStatusCode) {
					var content = await response.Content.ReadAsStringAsync ();
                    Items = JsonConvert.DeserializeObject <List<Model>> (content);
				}
			} catch (Exception ex) {
				Debug.WriteLine (@"				ERROR {0}", ex.Message);
			}

			return Items;
		}

		public async Task SaveTodoItemAsync (Model item, bool isNewItem = false)
		{
			var uri = new Uri (string.Format (Constants.RestUrl, item.ID));

			try {
				var json = JsonConvert.SerializeObject (item);
				var content = new StringContent (json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;
				if (isNewItem) {
					response = await client.PostAsync (uri, content);
				} else {
					response = await client.PutAsync (uri, content);
				}
				
				if (response.IsSuccessStatusCode) {
					Debug.WriteLine (@"				TodoItem successfully saved.");
				}
				
			} catch (Exception ex) {
				Debug.WriteLine (@"				ERROR {0}", ex.Message);
			}
		}

		public async Task DeleteTodoItemAsync (string id)
		{
			// RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
			var uri = new Uri (string.Format (Constants.RestUrl, id));

			try {
				var response = await client.DeleteAsync (uri);

				if (response.IsSuccessStatusCode) {
					Debug.WriteLine (@"				TodoItem successfully deleted.");	
				}
				
			} catch (Exception ex) {
				Debug.WriteLine (@"				ERROR {0}", ex.Message);
			}
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
					return JsonConvert.DeserializeObject<User>(result);
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
			menuItems = new List<Menu>();
			var uri = new Uri(Constants.RestUrl + "menu");

			try {
				var response = await client.GetAsync(uri);

				Debug.WriteLine(response);
				//if (response.IsSuccessStatusCode) {
				//	var content = await response.Content.ReadAsStringAsync();
				//	menuItems = JsonConvert.DeserializeObject<List<Menu>> (content);
				//	Debug.WriteLine(menuItems);
				//}

			} catch (Exception ex) {
				Debug.WriteLine (@"				ERROR {0}", ex.Message);
			}
			return menuItems;
		}
    }
}
