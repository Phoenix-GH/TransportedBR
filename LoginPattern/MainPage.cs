using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace LoginPattern
{
	public class MainPage : MasterDetailPage
	{
		public MainPage ()
		{
			Master = new MenuPage (this);
			Detail = new DetailPage ();
		}
	}
}

