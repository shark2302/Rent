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
    public class BuildingService : IBuildingService
    {
        private IUnitOfWork _database;

        public BuildingService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }


        public void CreateBuilding(BuildingDTO building)
        {
            var result = _database.Streets.Select().Include(s => s.City)
                .Where(s => s.Name == building.StreetName && s.City.Name == building.CityName)
                .ToList();
            var street = result[0];
            _database.Buildings.Create(new Building { Number = building.Number, StreetId = street.Id });
            _database.Save();
        }

        public BuildingDTO GetBuildingByNumberInStreet(string city, string street, int number)
        {
            var building = _database.Buildings.Select().Include(s => s.Street).Include(s => s.Street.City)
                .Where(s => s.Street.Name == street && s.Street.City.Name == city && s.Number == number)
                .ToList()[0];
            return new BuildingDTO(building);
        }

        public IEnumerable<BuildingDTO> GetBuildings()
        {
            List<BuildingDTO> result = new List<BuildingDTO>();
            foreach (var building in _database.Buildings.Select().Include(s => s.Street).Include(s => s.Street.City).ToList())
            {
                result.Add(new BuildingDTO(building));
            }
            return result;
        }
    }
}

