using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private RentContext db;
        
        private IRepository<City> _cityRepo;
        private IRepository<Street> _streetRepo;
        private IRepository<Building> _buildingRepo;
        private IRepository<Client> _clientRepo;
        private IRepository<Manager> _managerRepo;
        private IRepository<Product> _productRepo;
        private IRepository<ProductPrice> _priceRepo;
        private IRepository<Rent> _rentRepo;
        private IRepository<RentStore> _rentStoreRepo;

        public UnitOfWork(string connectionString)
        {
            db = new RentContext(connectionString);
        }

        public IRepository<City> Cities
        {
            get
            {
                if (_cityRepo == null)
                    _cityRepo = new Repository<City>(db);
                return _cityRepo;
            }
        }
        public IRepository<Street> Streets
        {
            get
            {
                if (_streetRepo == null)
                    _streetRepo = new Repository<Street>(db);
                return _streetRepo;
            }
        }

        public IRepository<Building> Buildings
        {
            get
            {
                if (_buildingRepo == null)
                    _buildingRepo = new Repository<Building>(db);
                return _buildingRepo;
            }
        }

        public IRepository<Client> Clients
        {
            get
            {
                if (_clientRepo == null)
                    _clientRepo = new Repository<Client>(db);
                return _clientRepo;
            }
        }

        public IRepository<Manager> Managers
        {
            get
            {
                if (_managerRepo == null)
                    _managerRepo = new Repository<Manager>(db);
                return _managerRepo;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                if (_productRepo == null)
                    _productRepo = new Repository<Product>(db);
                return _productRepo;
            }
        }

        public IRepository<ProductPrice> Prices
        {
            get
            {
                if (_priceRepo == null)
                    _priceRepo = new Repository<ProductPrice>(db);
                return _priceRepo;
            }
        }

        public IRepository<Rent> Rents
        {
            get
            {
                if (_rentRepo == null)
                    _rentRepo = new Repository<Rent>(db);
                return _rentRepo;
            }
        }

        public IRepository<RentStore> RentStores 
        {
            get
            {
                if (_rentStoreRepo== null)
                    _rentStoreRepo = new Repository<RentStore>(db);
                return _rentStoreRepo;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
