﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using ReCircle.Model.Adapter;

namespace ReCircle.Services
{
    public class ProductService
    {
        public ProductService()
        {
        }

        private readonly string ChildName = "Products";

        readonly FirebaseClient firebase = new FirebaseClient("https://recircle-d8492.firebaseio.com/");

        public async Task<List<Product>> GetAllProducts()
        {
            return (await firebase
                .Child(ChildName)
                .OnceAsync<Product>()).Select(item => new Product
                {
                    Name = item.Object.Name,
                    ProductId = item.Object.ProductId,
                    Description = item.Object.Description,
                    Price = item.Object.Price,
                    Stock = item.Object.Stock
                }).ToList();
        }

        public async Task AddProduct(string name, string Description, int Price, int Stock, string imageStream, int type)
        {            
            Guid guid = Guid.NewGuid();
            await firebase
                .Child(ChildName)
                .Child(guid.ToString())
                .PostAsync(new Product() {ProductId = guid, Name = name, Description = Description , Price = Price, Stock = Stock ,UrlImage = imageStream, Type =type });
        }

        public async Task<Product> GetProduct(Guid ProductId)
        {
            var allProducts = await GetAllProducts();
            await firebase
                .Child(ChildName)
                .OnceAsync<Product>();
            return allProducts.FirstOrDefault(a => a.ProductId == ProductId);
        }

        public async Task<Product> GetProduct(string name)
        {
            var allProducts = await GetAllProducts();
            await firebase
                .Child(ChildName)
                .OnceAsync<Product>();
            return allProducts.FirstOrDefault(a => a.Name == name);
        }

        public async Task UpdateProduct(Guid ProductId, string name, string Description, int Stock, int Price, string imageStream, int type)
        {
            var toUpdateProduct = (await firebase
                .Child(ChildName)
                .OnceAsync<Product>()).FirstOrDefault(a => a.Object.ProductId == ProductId);
            
            await firebase
                .Child(ChildName)
                .Child(toUpdateProduct.Key)
                .PutAsync(new Product() { ProductId = ProductId, Name = name,Description = Description,
                    Stock = Stock,  Price = Price , UrlImage = imageStream,Type=type});
        }

        public async Task DeleteProduct(Guid ProductId)
        {
            var toDeleteProduct = (await firebase
                .Child(ChildName)
                .OnceAsync<Product>()).FirstOrDefault(a => a.Object.ProductId == ProductId);
            await firebase.Child(ChildName).Child(toDeleteProduct.Key).DeleteAsync();
        }
    }
}
