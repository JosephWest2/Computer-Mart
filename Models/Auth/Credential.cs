using System.ComponentModel.DataAnnotations;

namespace Computer_Mart.Models.Auth
{
    public class Credential
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}