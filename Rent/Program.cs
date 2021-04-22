using System;
using System.Linq;
using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Rent
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server = (localdb)\\mssqllocaldb; Database = RentDb; Trusted_Connection = True;";
            UnitOfWork worker = new UnitOfWork(connectionString);
            ICityService cityService = new CityService(worker);
            IStreetService streetService = new StreetService(worker);
            IBuildingService buildingService = new BuildingService(worker);
            IClientService clientService = new ClientService(worker);
            IProductService productService = new ProductService(worker);
            IRentStoreService rentStoreService = new RentStoreService(worker);
            IProductPriceService productPriceService = new ProductPriceService(worker);
            IManagerService managerService = new ManagerService(worker);
           
        }

    }
}