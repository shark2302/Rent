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
    public class RentService : IRentService
    {
        private IUnitOfWork _database;

        public RentService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public void CreateRent(RentDTO rent)
        {
            var clientQuery = _database.Clients.Get(p => p.Name == rent.ClientName).GetEnumerator();
            var client = clientQuery.MoveNext() ? clientQuery.Current : null;
            var productQuery = _database.Products.Get(p => p.Name == rent.Product.ProductName).GetEnumerator();
            var product = productQuery.MoveNext() ? productQuery.Current : null;
            var rentStoreQuery = _database.RentStores.Get(p => p.Name == rent.RentStoreName).GetEnumerator();
            var rentStore = rentStoreQuery.MoveNext() ? rentStoreQuery.Current : null;
            var managerQuery = _database.Managers.Get(p => p.Name == rent.ManagerName).GetEnumerator();
            var manager = managerQuery.MoveNext() ? managerQuery.Current : null;
            _database.Rents.Create(new Rent
            {
                ClientId = client.Id,
                RentStoreId = rentStore.Id,
                ManagerId = manager.Id,
                ProductId = product.Id,
                StartTime = DateTime.Now
            });
            _database.Save();
        }

        public RentDTO GetRentById(int id)
        {
            var rent = _database.Rents.Select().Include(p => p.Client).Include(p => p.Manager).Include(p => p.RentStore).Include(p => p.Product)
                .Where(p => p.Id == id).ToList()[0];
            var price = _database.Prices.Select().Include(p => p.Product).Include(p => p.RentStore)
                .Where(p => p.ProductId == rent.ProductId).ToList()[0];
            return new RentDTO(rent, price);
        }

        public void StopRent(int id)
        {
            var rent = _database.Rents.FindById(id);
            rent.EndTime = DateTime.Now;
            _database.Rents.Update(rent);
            _database.Save();
        }
    }
}
