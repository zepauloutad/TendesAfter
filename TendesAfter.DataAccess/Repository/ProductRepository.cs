using TendesAfter.DataAccess.Repository.IRepository;
using TendesAfter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendesAfter.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.id == obj.id);
            if(objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.CoverType = obj.CoverType;
                if(obj.ImageUrl!= null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
                  
            }
        }

    }
}
