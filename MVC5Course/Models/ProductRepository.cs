using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => p.IsDeleted == false);
        }

        public override void Delete(Product entity)
        {
            entity.IsDeleted = true;
        }

        public Product Find(int id)
        {
            ////return this.Find(id);
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public IQueryable<Product> Get所有產品_依據ProductId大到小排序(int pageSize)
        {
            return this.All().OrderByDescending(p => p.ProductId).Take(10);
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}