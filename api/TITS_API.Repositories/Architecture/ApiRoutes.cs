﻿using System;
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
        public const string ProductsGetProductFromPwS = "api/products/getFromPwS";
        public const string ProductsGetProductNames = "api/products/getProductNames";
        public const string ProductsGetFullProductInfo = "api/products/getFullProductInfo";
        public const string ProductsAddProduct = "api/products";
        public const string ProductsUpdateProduct = "api/products";

        //Ingredients
        public const string IngredientsGetIngredientById = "api/ingredients/getById";
        public const string IngredientsGetIngredientByName = "api/ingredients/getByName";
        public const string IngredientsGetAllIngredients = "api/ingredients";
        public const string IngredientsAddIngredient = "api/ingredients";
        public const string IngredientsUpdateIngredient = "api/ingredients";
        public const string IngredientsPubChemAutocompleteTest = "api/ingredients/pubChemAutocompleteTest";
    }
}
