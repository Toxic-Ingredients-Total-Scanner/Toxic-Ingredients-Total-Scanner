using System;
using System.Collections.Generic;
using System.Text;

namespace TITS_API.Repositories.Architecture
{
    public class ApiRoutes
    {
        //Products
        public const string ProductsGetProductById = "api/products/getById";
        public const string ProductsGetProductByEan = "api/products/getByEan";
        public const string ProductsGetProductByName = "api/products/getByName";
        public const string ProductsAddProduct = "api/products";
        public const string ProductsUpdateProduct = "api/products";
    }
}
