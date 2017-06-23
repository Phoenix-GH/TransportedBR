using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginPattern
{
	public interface IRestService
	{
        Task<User> Login(Login user);
		Task<IEnumerable<Menu>> getMenu();
		Task<Dictionary<string, Object>> getModels(string model);
		Task<Dictionary<string, Object>> getModelInfo(string model);
		Task<Dictionary<string, Object>> postModels(string model, Object postData);
	}
}
