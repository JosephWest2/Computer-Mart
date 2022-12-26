using Computer_Mart.Data;
using Computer_Mart.Models;
using Computer_Mart.Models.Auth;
using Computer_Mart.Models.Cart;
using Computer_Mart.Models.Order;
using Computer_Mart.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Computer_Mart.Controllers
{
	
	public class CartController : Controller
	{
		private readonly Computer_MartContext _context;

		public CartController(Computer_MartContext context)
		{
			_context = context;
		}
        [Authorize]
        public IActionResult Index()
		{
			var cartInstance = HttpContext.Session.Get<ShoppingCart>(Constants.SessionCartString);

            string stringId = User.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
            int userId = int.Parse(stringId);

            float funds = _context.Users.FirstOrDefault(u => u.Id == userId).Funds;
            ViewData["Funds"] = funds.ToString("C");

            return View(cartInstance);
		}

		public IActionResult Add(int id)
		{
			var computer = _context.Computer.Find(id);

			if (computer == null)
			{
				return NotFound();
			}
			
			var cartInstance = HttpContext.Session.Get<ShoppingCart>(Constants.SessionCartString);
			
			if (cartInstance == null)
			{
				cartInstance = new ShoppingCart()
				{
					Items = new List<CartItem>()
				};
			}
			bool match = false;
			foreach (var item in cartInstance.Items)
			{
				if (item.Computer.Id == id)
				{
					item.Quantity += 1;
					match = true;
				}
			}
			if (!match)
			{
				cartInstance.Items.Add(new CartItem()
				{
					Computer = computer,
					Quantity = 1
				});
			}
			

			HttpContext.Session.Set<ShoppingCart>(Constants.SessionCartString, cartInstance);
			TempData["Alert"] = "Item added.";

			return Redirect(Request.Headers.Referer.ToString());
		}

        public IActionResult Increase(int id)
        {
            var cartInstance = HttpContext.Session.Get<ShoppingCart>(Constants.SessionCartString);

			if (cartInstance == null)
			{
				return NotFound();
			}

            foreach (var item in cartInstance.Items)
            {
				Console.WriteLine(item.Computer.Id);
				Console.WriteLine(id);
                if (id == item.Computer.Id)
                {
					item.Quantity += 1;
                }
            }
            HttpContext.Session.Set<ShoppingCart>(Constants.SessionCartString, cartInstance);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Decrease(int id)
		{
			var cartInstance = HttpContext.Session.Get<ShoppingCart>(Constants.SessionCartString);

            if (cartInstance == null)
            {
                return NotFound();
            }

			foreach (var item in cartInstance.Items)
			{
				if (id == item.Computer.Id)
				{
					item.Quantity -= 1;

					if (item.Quantity <= 0)
					{
						cartInstance.Items.Remove(item);
						break;
					}
				}
			}
            HttpContext.Session.Set<ShoppingCart>(Constants.SessionCartString, cartInstance);
            EmptyCartCheck(HttpContext);

            return RedirectToAction(nameof(Index));
		}

		public IActionResult Remove(int id)
		{
			var cartInstance = HttpContext.Session.Get<ShoppingCart>(Constants.SessionCartString);

            if (cartInstance == null)
            {
                return NotFound();
            }

			foreach (var item in cartInstance.Items)
			{
				if (id == item.Computer.Id)
				{
					cartInstance.Items.Remove(item);
					break;
				}
			}
            HttpContext.Session.Set<ShoppingCart>(Constants.SessionCartString, cartInstance);
			EmptyCartCheck(HttpContext);

            return RedirectToAction(nameof(Index));
		}

		public IActionResult Clear()
		{
			HttpContext.Session.Remove(Constants.SessionCartString);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Checkout()
		{
			

			var cartInstance = HttpContext.Session.Get<ShoppingCart>(Constants.SessionCartString);

            string stringId = User.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
            int userId = int.Parse(stringId);

			float funds = _context.Users.FirstOrDefault(u => u.Id == userId).Funds;
			if (funds < cartInstance.PriceTotal)
			{
				TempData["Alert"] = "More funds needed.";
				return RedirectToAction("Index");
			}


            Order order = new Order()
			{
				Id = Guid.NewGuid().ToString(),
				Total = cartInstance.PriceTotal,
				UserId = userId,
				Date = DateTime.Now
			};
			_context.Add(order);
			_context.SaveChanges();

			foreach (CartItem item in cartInstance.Items)
			{
				OrderItem orderItem = new OrderItem()
				{
					ProductName = item.Computer.Name,
					Quantity = item.Quantity,
					Price = item.PriceTotal,
					OrderId = order.Id
				};
				_context.Add(orderItem);
				_context.SaveChanges();

			}

			User user = _context.Users.FirstOrDefault(u => u.Id == userId);
			user.Funds -= cartInstance.PriceTotal;

			_context.Update(user);
			_context.SaveChanges();

            HttpContext.Session.Remove(Constants.SessionCartString);
			TempData["Alert"] = "Purchase successful.";
            return RedirectToAction("Index", "Orders");
		}

		public static void EmptyCartCheck(HttpContext httpContext)
		{
			var cartInstance = httpContext.Session.Get<ShoppingCart>(Constants.SessionCartString);
			if (cartInstance != null && cartInstance.Items.Count == 0)
			{
				httpContext.Session.Remove(Constants.SessionCartString);
			}
			
        }

	}
}
