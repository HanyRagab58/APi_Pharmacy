﻿using Demo.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Specification
{
    public class ProductsWithFilltrForCountSpecifiaction:BaseSpecification<Product>
    {
        public ProductsWithFilltrForCountSpecifiaction(ProductSpecParams productParams)
           : base(x =>
           (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
           (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
           (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
        {

        }
    }
}
