using Demo.BLL.Entities;
using Demo.DAL.Data;
using Demo.DAL.Inerfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL
{
    public class ProductRepository : IProductRepository
    {
        private StoreContext _Context;

        public ProductRepository(StoreContext context)
        {
            
            _Context = context;
        }

        public StoreContext Context { get; }

        public async Task<IReadOnlyList<PicturBrand>> GetPicturBrandsAsync()
        => await _Context.PicturBrands.ToListAsync();

        public async Task<IReadOnlyList<PicturType>> GetPicturTypesAsync()
        => await _Context.PicturTypes.ToListAsync();

        public async Task<Product> GetProductById(int id)
        => await _Context.Products.Include(p=>p.PicturType).
             Include(p=>p.PicturBrand).
             FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        => await _Context.Products.Include(p=>p.PicturType).Include(p=>p.PicturBrand).ToListAsync();
    }
}
