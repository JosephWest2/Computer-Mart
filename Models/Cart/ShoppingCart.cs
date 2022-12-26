using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Computer_Mart.Models.Cart
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; }
        [DataType(DataType.Currency)]
        public float PriceTotal
        {
            get
            {
                float total = 0;
                Items.ForEach(cartItem =>
                {
                    total += cartItem.PriceTotal;
                });
                return total;
            }
        }

    }
}
