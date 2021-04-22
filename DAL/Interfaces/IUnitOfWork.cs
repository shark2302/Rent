using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<City> Cities { get;}
        IRepository<Street> Streets { get;}
        IRepository<Building> Buildings { get;}
        IRepository<Client> Clients { get;}
        IRepository<Manager> Managers { get;}
        IRepository<Product> Products { get;}
        IRepository<ProductPrice> Prices { get;}
        IRepository<Rent> Rents { get; }
        IRepository<RentStore> RentStores { get;}
        void Save();
    }
}
