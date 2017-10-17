using Rodrigo_banach.Context;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Rodrigo_banach.Models;

namespace Rodrigo_banach.Controllers
{
    public class ProductsController : Controller
    {

        private readonly EFContext _context = new EFContext();

        // GET: Produtos
        public ActionResult Index()
        {
            var list = _context
                .Products
                .Include(c => c.Category)
                .Include(c => c.Supplier)
                .OrderBy(p => p.Name).ToList();
            return View(list);
        }

        public ActionResult Create()

        {
            ViewBag.CategoryId =
                new SelectList(_context.Categories.OrderBy(b => b.Name),
                "CategoryId", "Name");

            ViewBag.SupplierId = new SelectList(_context.Suppliers.OrderBy(b => b.Name),
                "SupplierId", "Name");

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Suppliers.Add(product);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

        }
    }
}