using Computer_Mart.Models.Auth;
using System.ComponentModel.DataAnnotations;

namespace Computer_Mart.Models.Order
{
    public class Order
    {
        public string Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Currency)]
        public float Total { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
