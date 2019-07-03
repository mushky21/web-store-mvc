using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Data;
using webStoreFinal.Models;

namespace webStoreFinal.Services
{
    public class ProductRepository:IProductRepository
    {
        private myStoreDbContext _storeDbContext;

        public ProductRepository(myStoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public Product FindProduct(int productId)
        {
            var foundProduct = Products().Find((product) => product.ProductKey == productId);
            return foundProduct;
        }

        public async Task<bool> AddProduct(Product newProduct, IFormFile [] pictures)
        {
            for (int i = 0; i < pictures.Length; i++)
            {
                using (var msStream = new MemoryStream())
                {
                    if (pictures[i] != null)
                    {
                        await pictures[i].CopyToAsync(msStream);
                        if (i==0)
                        {
                            newProduct.Photo1 = msStream.ToArray();
                        }
                        else if (i == 1)
                        {
                            newProduct.Photo2 = msStream.ToArray();
                        }
                        if (i == 2)
                        {
                            newProduct.Photo3 = msStream.ToArray();
                        }

                    }
                }
            }
            _storeDbContext.Products.Add(newProduct);
     
            int addedRows = _storeDbContext.SaveChanges();
            return addedRows > 0;
        }

        public bool RemoveProduct(int productId)
        {
            var product = _storeDbContext.Products.First(p => p.ProductKey == productId);
            _storeDbContext.Products.Remove(product);
            int deleteRows = _storeDbContext.SaveChanges();
            return deleteRows > 0;
        }

        public List<Product> Products()
        {
            return _storeDbContext.Products.ToList();
        }

        // general note 1:since database approaches are expensive we chose to seperate updating product buyer and state to different methods because 
        // wanted to approach only specific datacontext properties that were needed to be updated

        public bool UpdateProductBuyer(int productId,string buyerId)
        {
            var updated = FindProduct(productId);
            updated.BuyerId = buyerId;
            int updatedRows = _storeDbContext.SaveChanges();
            return updatedRows > 0;
        }
        //public bool UpdateProductBuyer(Product updatedProduct)
        //{
        //    var updated = _storeDbContext.Products.First(p => p.ProductKey == updatedProduct.ProductKey);
        //    updated.BuyerId = updatedProduct.BuyerId;
        //    int updatedRows = _storeDbContext.SaveChanges();
        //    return updatedRows > 0;
        //}

        public bool UpdateProductState(int id,State state)
        {
            //search the wanted item in the repository and change it's status from available to in cart
            int updated = default(int);
            var foundProduct = FindProduct(id);
            if (foundProduct!=null)
            {
                foundProduct.ProductState = State.Available;
                updated = _storeDbContext.SaveChanges();
            }
            return updated > 0;
        }

        //general note 2: since the repository class is used to manage the databasecontext we decided to implement all the linq queries here


        public List<Product> OrderByTitle()
        {
            var orderByTitle = from product in Products()
                               where product.ProductState == State.Available
                               orderby product.Title ascending
                               select product;
            return orderByTitle.ToList();

        }

        public List<Product> OrderByDate()
        {
            var orderByDate = from product in Products()
                              where product.ProductState == State.Available
                              orderby product.PublishedDate
                              select product;
            return orderByDate.ToList();

        }



        public List<Product> AvailableItems()
        {
            var availableItems = from product in Products()
                                 where product.ProductState == State.Available
                                 select product;


            return availableItems.ToList();
        }

    }
}
