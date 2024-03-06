﻿namespace BookStore.Web.Models.AuthDtos
{
	public class UserDto
	{
		public string ID { get; set; }

		public string Email { get; set; }

		public string FullName { get; set; }

		public string PhoneNumber { get; set; }

		public DateTime BrithData { get; set; }
	}
}
