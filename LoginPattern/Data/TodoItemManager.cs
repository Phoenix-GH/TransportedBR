using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginPattern
{
	public class TodoItemManager
	{
		IRestService restService;

		public TodoItemManager (IRestService service)
		{
			restService = service;
		}

        public Task<List<Model>> GetTasksAsync ()
		{
			return restService.RefreshDataAsync ();	
		}

        public Task SaveTaskAsync (Model item, bool isNewItem = false)
		{
			return restService.SaveTodoItemAsync (item, isNewItem);
		}

        public Task DeleteTaskAsync (Model item)
		{
			return restService.DeleteTodoItemAsync (item.ID);
		}
	}
}
