using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Computer_Mart.Models.Order
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
		[DisplayName("Product")]
		public string ProductName { get; set; }
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        public float Price { get; set; }
        [ForeignKey(nameof(Order))]
        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
