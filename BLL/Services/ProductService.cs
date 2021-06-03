using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class ProductService : IProductService
    {

        private IUnitOfWork _database;

        public ProductService(IUnitOfWork database)
        {
            _database = database;
        }

        public void CreateProduct(ProductDTO productDTO)
        {
            _database.Products.Create(new Product { Name = productDTO.Name });
            _database.Save();
        }

        public ProductDTO GetById(int id)
        {
            return new ProductDTO(_database.Products.FindById(id));
        }

        public ProductDTO FindByName(string name)
        {
            var result = _database.Products.Get(s => s.Name == name).GetEnumerator();
            return result.MoveNext() ? new ProductDTO(result.Current) : null;
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(_database.Products.Get());
        }

        public void DeleteProduct(int id)
        {
            _database.Products.Remove(_database.Products.FindById(id));
            _database.Save();
        }
    }
}
