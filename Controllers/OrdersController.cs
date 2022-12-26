using Computer_Mart.Data;
using Computer_Mart.Models.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            var orders = _context.Orders.Include(o => o.User).ToList().Where(o => o.UserId == userId);

            return View(orders);
        }

        public IActionResult Details(string Id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == Id);
            ViewBag.order = order;
            ViewData["priceString"] = order.Total.ToString("C");

            var orderItems = _context.OrderItems.ToList().Where(oi => oi.OrderId == Id);

            return View(orderItems);
        }
    }
}
