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
            var rentStore = _database.RentStores.FindById(rent.RentStoreId);
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

        public List<RentDTO> GetAllActiveRentsForClient(int clientId)
        {
            List<RentDTO> res = new List<RentDTO>();
            var rents = _database.Rents.Select().Include(p => p.Client).Include(p => p.Manager).Include(p => p.RentStore).Include(p => p.Product)
                .Where(p => p.ClientId == clientId && p.EndTime == DateTime.MinValue);
            foreach (var rent in rents.ToList())
            {
                var price = _database.Prices.Select().Include(p => p.Product).Include(p => p.RentStore)
                                            .Where(p => p.ProductId == rent.ProductId).ToList()[0];
                res.Add(new RentDTO(rent, price));
            }
            return res;
        }


        public List<RentDTO> GetAllActiveRentsForManager(int managerId)
        {
            List<RentDTO> res = new List<RentDTO>();
            var rents = _database.Rents.Select().Include(p => p.Client).Include(p => p.Manager).Include(p => p.RentStore).Include(p => p.Product)
                .Where(p => p.ManagerId == managerId && p.EndTime == DateTime.MinValue);
            foreach (var rent in rents.ToList())
            {
                var price = _database.Prices.Select().Include(p => p.Product).Include(p => p.RentStore)
                                            .Where(p => p.ProductId == rent.ProductId).ToList()[0];
                res.Add(new RentDTO(rent, price));
            }
            return res;
        }

        public List<RentDTO> GetAllActiveRentsForStore(int rentStoreId)
        {
            List<RentDTO> res = new List<RentDTO>();
            var rents = _database.Rents.Select().Include(p => p.Client).Include(p => p.Manager).Include(p => p.RentStore).Include(p => p.Product)
                .Where(p => p.RentStoreId == rentStoreId && p.EndTime == DateTime.MinValue);
            foreach(var rent in rents.ToList())
            {
                var price = _database.Prices.Select().Include(p => p.Product).Include(p => p.RentStore)
                                            .Where(p => p.ProductId == rent.ProductId).ToList()[0];
                res.Add(new RentDTO(rent, price));
            }
            return res;
        }

        public List<RentDTO> GetAllEndedRentsForClient(int clientId)
        {
            List<RentDTO> res = new List<RentDTO>();
            var rents = _database.Rents.Select().Include(p => p.Client).Include(p => p.Manager).Include(p => p.RentStore).Include(p => p.Product)
                .Where(p => p.ClientId == clientId && p.EndTime != DateTime.MinValue);
            foreach (var rent in rents.ToList())
            {
                var price = _database.Prices.Select().Include(p => p.Product).Include(p => p.RentStore)
                                            .Where(p => p.ProductId == rent.ProductId).ToList()[0];
                res.Add(new RentDTO(rent, price));
            }
            return res;
        }

        public List<RentDTO> GetAllEndedRentsForManager(int managerId)
        {
            List<RentDTO> res = new List<RentDTO>();
            var rents = _database.Rents.Select().Include(p => p.Client).Include(p => p.Manager).Include(p => p.RentStore).Include(p => p.Product)
                .Where(p => p.ManagerId == managerId && p.EndTime != DateTime.MinValue);
            foreach (var rent in rents.ToList())
            {
                var price = _database.Prices.Select().Include(p => p.Product).Include(p => p.RentStore)
                                            .Where(p => p.ProductId == rent.ProductId).ToList()[0];
                res.Add(new RentDTO(rent, price));
            }
            return res;
        }

        public List<RentDTO> GetAllEndedRentsForStore(int rentStoreId)
        {

            List<RentDTO> res = new List<RentDTO>();
            var rents = _database.Rents.Select().Include(p => p.Client).Include(p => p.Manager).Include(p => p.RentStore).Include(p => p.Product)
                .Where(p => p.RentStoreId == rentStoreId && p.EndTime != DateTime.MinValue);
            foreach (var rent in rents.ToList())
            {
                var price = _database.Prices.Select().Include(p => p.Product).Include(p => p.RentStore)
                                            .Where(p => p.ProductId == rent.ProductId).ToList()[0];
                res.Add(new RentDTO(rent, price));
            }
            return res;
        }

        public List<RentDTO> GetFilteredRents(int rentStoreId, int clientId, int managerId, string productName, DateTime from, DateTime to)
        {
            List<RentDTO> res = new List<RentDTO>();
            var rents = _database.Rents.Select().Include(p => p.Client).Include(p => p.Manager).Include(p => p.RentStore).Include(p => p.Product)
                .Where(p => p.RentStoreId == rentStoreId && p.EndTime != DateTime.MinValue);
            
            if(clientId != 0)
            {
                rents = rents.Where(p => p.ClientId == clientId);
            }
            if (managerId != 0)
                rents = rents.Where(p => p.ManagerId == managerId);
            if (!String.IsNullOrEmpty(productName))
                rents = rents.Where(p => p.Product.Name == productName);
            if (from != DateTime.MinValue)
                rents = rents.Where(p => p.EndTime > from);
            if (to != DateTime.MinValue)
                rents = rents.Where(p => p.EndTime < to);
            foreach (var rent in rents.ToList())
            {
                var price = _database.Prices.Select().Include(p => p.Product).Include(p => p.RentStore)
                                            .Where(p => p.ProductId == rent.ProductId).ToList()[0];
                res.Add(new RentDTO(rent, price));
            }
            return res;
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
