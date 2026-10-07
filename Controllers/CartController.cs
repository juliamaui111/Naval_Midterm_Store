using Microsoft.AspNetCore.Mvc;
using Naval_Midterm.Data;
using Naval_Midterm.Models;

namespace naval_Midterm.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var items = _context.CartItems.ToList();
            return View(items);
        }
        public IActionResult Add(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            var item = _context.CartItems.FirstOrDefault(x => x.ProductId == product.Id);

            if (item != null)
            {
                item.Quantity++;

            }
            else
            {
                item = new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            
            };
            _context.CartItems.Add(item);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
       public IActionResult Remove(int id)
       {
        var item = _context.CartItems.Find(id);

        if (item == null)
        {
            return NotFound();
        }
        _context.CartItems.Remove(item);
        _context.SaveChanges();

        return RedirectToAction("Index");
       }
       [HttpPost]
       public IActionResult Update(int id, int quantity)
       {
        var item = _context.CartItems.Find(id);
        if (item == null)
        {
            return NotFound();
        }
        item.Quantity = quantity;
        _context.SaveChanges();
        return RedirectToAction("Index");
       }
    }
}