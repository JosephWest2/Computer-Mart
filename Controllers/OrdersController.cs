using Computer_Mart.Data;
using Computer_Mart.Models.Order;
using Microsoft.AspNetCore.Mvc;

namespace Computer_Mart.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Computer_MartContext _context;

        public OrdersController(Computer_MartContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string stringId = User.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;
            int userId = int.Parse(stringId);

            var orders = _context.Orders.ToList().Where(o => o.User.Id == userId);

            return View(orders);
        }
    }
}
