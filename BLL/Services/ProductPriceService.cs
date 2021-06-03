using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class ProductPriceService : IProductPriceService
    {

        private IUnitOfWork _database;

        public ProductPriceService(IUnitOfWork database)
        {
            _database = database;
        }

        public void CreateProductPrice(ProductPriceDTO pruductPriceDTO)
        {
            var res1 = _database.Products.Get(p => p.Name == pruductPriceDTO.ProductName).GetEnumerator();
            var product = res1.MoveNext() ? res1.Current : null;
            if (product == null)
                throw new NullReferenceException("Такого продукта не существует");
            var rentStore = _database.RentStores.FindById(pruductPriceDTO.RentStoreId);
            _database.Prices.Create(new ProductPrice { Price = pruductPriceDTO.Price, ProductId = product.Id, RentStoreId = rentStore.Id });
            _database.Save();
        }

        public IEnumerable<ProductPriceDTO> GetAllForStore(string storeName)
        {
            List<ProductPriceDTO> result = new List<ProductPriceDTO>();
            foreach(var price in _database.Prices.Select().Include(p => p.Product).Include(p => p.RentStore)
                .Where(p => p.RentStore.Name == storeName))
            {
                result.Add(new ProductPriceDTO(price));
            }
            return result;
        }

        public IEnumerable<ProductPriceDTO> GetAllForStore(int storeId)
        {
            List<ProductPriceDTO> result = new List<ProductPriceDTO>();
            foreach (var price in _database.Prices.Select().Include(p => p.Product).Include(p => p.RentStore)
                .Where(p => p.RentStore.Id == storeId))
            {
                result.Add(new ProductPriceDTO(price));
            }
            return result;
        }

        public ProductPriceDTO GetByProductAndStore(string productName, string rentStoreName)
        {
            var price = _database.Prices.Select().Include(p => p.Product).Include(p => p.RentStore)
                .Where(p => p.RentStore.Name == rentStoreName && p.Product.Name == productName).ToList()[0];

            return price != null ? new ProductPriceDTO(price) : null;

        }

        public ProductPriceDTO GetByProductAndStore(string productName, int rentStoreId)
        { 
            var price = _database.Prices.Select().Include(p => p.Product).Include(p => p.RentStore)
                .Where(p => p.RentStore.Id == rentStoreId && p.Product.Name == productName).ToList()[0];

            return price != null ? new ProductPriceDTO(price) : null;

        }


    }
}
