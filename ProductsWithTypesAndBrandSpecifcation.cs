using Demo.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Specification
{
    public class ProductsWithTypesAndBrandSpecifcation : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandSpecifcation(ProductSpecParams productParams) 
            : base(x=>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue||x.ProductBrandId == productParams.BrandId)&&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
        {
            AddInclude(p => p.PicturType);
            AddInclude(p => p.PicturBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex -1), productParams.PageSize);

            if(!string.IsNullOrEmpty(productParams.sort))
            {
                switch (productParams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                            break;
                    case "priceDesc":
                        AddOrderByDescinding(p => p.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                            break;
                }
            }
        }
        public ProductsWithTypesAndBrandSpecifcation(int id)
           : base(x =>x.Id==id)
        {
            AddInclude(p => p.PicturType);
            AddInclude(p => p.PicturBrand);
        }
    }
}
