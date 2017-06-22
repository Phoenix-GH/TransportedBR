using System;

namespace LoginPattern
{
	public class User
	{
		public string email { get; set; }

        public bool active { get; set; }

        public bool superadmin { get; set; }

		public string createdAt { get; set; }
		public string updatedAt { get; set; }

        public int id { get; set; }
        public string tokenLogin { get; set; }
	}
}
