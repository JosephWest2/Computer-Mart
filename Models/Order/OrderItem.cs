using System.ComponentModel.DataAnnotations;

namespace Computer_Mart.Models.Order
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        public float Price { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
