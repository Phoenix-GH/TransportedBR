using System;
using System.Collections.Generic;

namespace LoginPattern
{
    public class Config
    {
		public List<string> logo { get; set; }
		public List<string> menuimage { get; set; }
		public List<string> background { get; set; }
		public string color { get; set; }
		public string createdAt { get; set; }
		public string updatedAt { get; set; }
		public int id { get; set; }
		public string colorbtn { get; set; }
		public string contentbarcolor { get; set; }
		public string actionbuttoncolor { get; set; }
		public bool tls { get; set; }
		public bool ssl { get; set; }
		public string project_name { get; set; }
    }
}
