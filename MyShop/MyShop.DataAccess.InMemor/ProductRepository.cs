using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemor
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Products> products;


        public ProductRepository()
        {
            products = cache["products"] as List<Products>; 

            if( products == null)
            {
                products = new List<Products>();
            }
        }

        public void Commit()
        {

            cache["products"] = products;

        }

        public void Insert( Products newProduct)
        {
            products.Add(newProduct);
        }

        public void Update (Products updatedProduct)
        {
            Products productToUpdate = products.Find(p => p.Id == updatedProduct.Id); 

            if(productToUpdate != null)
            {
                productToUpdate = updatedProduct;
                
            }
            else
            {
                throw new Exception("Product not found");
            }


        }

        public Products Find( String Id)
        {
            Products producToFind = products.Find(p => p.Id == Id);

            if (producToFind != null)
            {
                return producToFind; 

            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Products> Collection()
        {
            return products.AsQueryable(); 
        }


        public void Delete ( string Id)
        {
            Products productToBeDelelted = products.Find(p => p.Id == Id);

            if (productToBeDelelted != null)
            {
                products.Remove(productToBeDelelted);

            }
            else
            {
                throw new Exception("Product not found");
            }
        }

    }
}
