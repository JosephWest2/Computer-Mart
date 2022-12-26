using Computer_Mart.Data;
using Computer_Mart.Models.Cart;
using Computer_Mart.Statics;
using Microsoft.AspNetCore.Mvc;

namespace Computer_Mart.Components
{
    public class MiniCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var currentCart = HttpContext.Session.Get<ShoppingCart>(Constants.SessionCartString);
            
            if (currentCart != null)
            {
                int count = 0;
                foreach (var item in currentCart.Items)
                {
                    count += item.Quantity;
                }
                ViewData["Count"] = count;
            }
            return View(currentCart);
        }
    }
}
