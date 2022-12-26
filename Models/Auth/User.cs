using System.ComponentModel.DataAnnotations;

namespace Computer_Mart.Models.Auth
{
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string PasswordHash { get; set; }
		public bool Admin { get; set; }
		[DataType(DataType.Currency)]
		public float Funds { get; set; }
	}
}
