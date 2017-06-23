using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginPattern
{
	public interface IRestService
	{
        Task<User> Login(Login user);
		Task<IEnumerable<Menu>> getMenu();
		Task<IEnumerable<Config>> getConfig();
		Task<Dictionary<string, Object>> getModels(string model);
		Task<Dictionary<string, Object>> getModelInfo(string model);
		Task<IEnumerable<Dictionary<string, Object>>> postModels(string model, string json);
	}
}
