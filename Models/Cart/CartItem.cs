using System.ComponentModel.DataAnnotations;

namespace Computer_Mart.Models.Cart
{
    public class CartItem
    {
        public int Quantity { get; set; }
        public Computer Computer { get; set; }
		[DataType(DataType.Currency)]
		public float PriceTotal
        {
            get { return Computer.Price * Quantity; }
        }

    }
}
