using Rodrigo_banach.Context;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Rodrigo_banach.Models;
using System.Net;

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
                .OrderBy(p => p.Name)
                .ToList();
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
        // POST: Produtos/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(product);
            }
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_context.Categories.
            OrderBy(b => b.Name), "CategoryId", "Name", product.
            CategoryId);
            ViewBag.SupplierId = new SelectList(_context.Suppliers.
            OrderBy(b => b.Name), "SupplierId", "Name", product.
            ProductId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Entry(product).State = EntityState.Modified;
                    _context.SaveChanges();

                    
                return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }
        }

        // GET: Produtos/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.
                BadRequest);
            }
            Product product = _context.Products.Where(p => p.ProductId ==
            id).Include(c => c.Category).Include(f => f.Supplier).
            First();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.
                BadRequest);
            }
            Product product = _context.Products.Where(p => p.ProductId ==
            id).Include(c => c.Category).Include(f => f.Supplier).
            First();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Produtos/Delete/5
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                Product product = _context.Products.Find(id);
               _context.Products.Remove(product);
                _context.SaveChanges();
                TempData["Message"] = "Produto " + product.Name.ToUpper()
            

            + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}