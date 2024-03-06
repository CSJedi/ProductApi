﻿using ProductApp.DAL.Models;

namespace ProductApp.DAL.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetProductById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
