using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;

        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }

        public List<Product> FindbycateID(long categoryID)
        {
            var model = db.Products.Where(x => x.Status == true && x.CategoryID == categoryID)
                          .OrderByDescending(x => x.CreatedDate)
                          .ToList();
            return model;
        }

        public Product FindbyproductID (long id)
        {
            return db.Products.FirstOrDefault(x => x.Status == true && x.ID == id);
        }

        public List<Product> ListRelatedProducts (long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }

        public List<Product> search (string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).ToList();
        }
    }
}
