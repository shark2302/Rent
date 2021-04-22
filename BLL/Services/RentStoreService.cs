using AutoMapper;
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
    public class RentStoreService : IRentStoreService
    {

        private IUnitOfWork _database;

        public RentStoreService(IUnitOfWork database)
        {
            _database = database;
        }

        public void CreateRentStore(RentStoreDTO rentStoreDTO)
        {
            var building = _database.Buildings.Select().Include(s => s.Street).Include(s => s.Street.City)
                .Where(s => s.Street.Name == rentStoreDTO.Building.StreetName && s.Street.City.Name == rentStoreDTO.Building.CityName
                && s.Number == rentStoreDTO.Building.Number)
                .ToList()[0];
            _database.RentStores.Create(new RentStore { Name = rentStoreDTO.Name, BuildingId = building.Id });
            _database.Save();
        }

        public RentStoreDTO GetRentStoreByNameAndAdress(string name, BuildingDTO buildingDTO)
        {
            var result = _database.RentStores.Select().Include(s => s.Building).Include(s => s.Building.Street)
                .Include(s => s.Building.Street.City).Where(s => s.Name == name && s.Building.Number == buildingDTO.Number
                && s.Building.Street.Name == buildingDTO.StreetName && s.Building.Street.City.Name == buildingDTO.CityName).ToList();
           

           
            return result.Count > 0 ? new RentStoreDTO(result[0], MapPrice(result[0])) : null;
        }

        public IEnumerable<RentStoreDTO> GetRentStores()
        {
            var result = new List<RentStoreDTO>();
            foreach (var store in _database.RentStores.Select().Include(s => s.Building).
                Include(s => s.Building.Street).Include(s => s.Building.Street.City).Include(s => s.Products).ToList())
            {
                result.Add(new RentStoreDTO(store, MapPrice(store)));
            }
            return result;
        }

        private List<ProductPriceDTO> MapPrice(RentStore rentStore)
        {
            List<ProductPriceDTO> prices = new List<ProductPriceDTO>();
            foreach (var price in rentStore.Products)
            {
                prices.Add(new ProductPriceDTO(price));
            }
            return prices;
        }
    }
}
