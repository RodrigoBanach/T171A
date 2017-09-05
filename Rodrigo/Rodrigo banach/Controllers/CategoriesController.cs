using Rodrigo_banach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rodrigo_banach.Controllers
{
    public class CategoriesController : Controller
    {

        private static IList<Category> categoryList =
        new List<Category>() 
    {
                new Category() { CategoryId = 1, Name = "Notebooks" },
                new Category() { CategoryId = 2, Name = "Monitores" },
                new Category() { CategoryId = 3, Name = "Impressoras" },
                new Category() { CategoryId = 4, Name = "Mouses" },
                new Category() { CategoryId = 5, Name = "Desktops" },

            };


        // GET: Category
        public ActionResult Index()
        {

            return View(categoryList);
        }


        public ActionResult Details(long id)
        {
            var category = categoryList
                .Where(c => c.CategoryId == id)
                .First();

            return View(category);

        }


        public ActionResult Create()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (Category category)
        {
            categoryList.Add(category);
            category.CategoryId =
               categoryList.Max(c => c.CategoryId) + 1;
            //category.Select(m => m.CategoryId).Max() + 1;
            return RedirectToAction("Create");
        }


        public ActionResult Edit(long id)
        {
            var category = categoryList
                .Where(c => c.CategoryId == id)
                .First();
            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category modified)
        {
            var category = categoryList
                .Where(c => c.CategoryId == modified.CategoryId)
                .First();

            category.Name = modified.Name;

        //    categoryList.Remove(category);
          //  categoryList.Add(modified);

            return RedirectToAction("Index");
        }


        public ActionResult Delete(long id)
        {
            var category = categoryList
                .Where(c => c.CategoryId == id)
                .First();
            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Category ToDelete)
        {
            var category = categoryList
                .Where(c => c.CategoryId == ToDelete.CategoryId)
                .First();

            categoryList.Remove(category);


            return RedirectToAction("Index");
        }
    }
}







