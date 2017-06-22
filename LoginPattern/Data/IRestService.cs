using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginPattern
{
	public interface IRestService
	{
		Task<List<Model>> RefreshDataAsync ();

		Task SaveTodoItemAsync (Model item, bool isNewItem);

		Task DeleteTodoItemAsync (string id);

        Task<User> Login(Login user);

		Task<IEnumerable<Menu>> getMenu();
	}
}
