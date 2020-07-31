using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemor;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository Context;

        public ProductManagerController()
        {
            Context = new ProductRepository();
        }


        // GET: ProductManager
        public ActionResult Index()
        {

            List<Products> products = Context.Collection().ToList();

            return View(products);
        }

        
        public ActionResult Create()
        {
            Products product = new Products();

            return View(product);
        }

        [HttpPost]
        public ActionResult Create( Products newProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(newProduct);  
            }
            else
            {
                Context.Insert(newProduct);
                Context.Commit();

                return RedirectToAction("Index");
            }
        }


        public ActionResult Edit(string Id)
        {
            Products product = Context.Find(Id); 
            if( product == null)
            {
                return HttpNotFound();  
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit (Products product, string id)
        {
            Products productToEdit = Context.Find(id);

            if( productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if(!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                Context.Commit();

                return RedirectToAction("Index"); 

            }

        }




        public ActionResult Delete(string id)
        {
            Products productToDelete = Context.Find(id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {

            Products productToDelete = Context.Find(id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                Context.Delete(id);
                Context.Commit();
                return RedirectToAction("Index");
            }
        }

    }
}