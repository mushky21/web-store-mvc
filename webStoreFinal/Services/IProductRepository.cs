﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Models;

namespace webStoreFinal.Services
{
    public interface IProductRepository
    {
        List<Product> Products();
        Product FindProduct(int productId);
        bool AddProduct(Product newProduct);
        bool RemoveProduct(int productId);
        bool UpdateProductBuyer(int productId, int buyerId);
        bool UpdateProductState(int id,State state);
        List<Product> OrderByTitle();
        List<Product> OrderByDate();
        List<Product> AvailableItems();
    }
}
