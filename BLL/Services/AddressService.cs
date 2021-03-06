using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BLL.Services
{
    public class AddressService : IAddressService
    {

        private IUnitOfWork _database;

        public AddressService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public void CreateCity(CityDTO city)
        {
            _database.Cities.Create(new City { Name = city.Name });
            _database.Save();
        }

        public CityDTO GetCityById(int cityId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<City, CityDTO>()).CreateMapper();
            return mapper.Map<City, CityDTO>(_database.Cities.FindById(cityId));
        }

        public IEnumerable<CityDTO> GetCities()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<City, CityDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<City>, List<CityDTO>>(_database.Cities.Get());
        }

        public CityDTO GetCityByName(string name)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<City, CityDTO>()).CreateMapper();
            var e =  mapper.Map<IEnumerable<City>, List<CityDTO>>(_database.Cities.Get(p => p.Name == name));
            return e.Count > 0 ? e[0] : null;
        }

        public void CreateStreet(StreetDTO street)
        {
            var result = _database.Cities.Get(c => c.Name == street.CityName).GetEnumerator();
            var city = result.MoveNext() ? result.Current : null;
            _database.Streets.Create(new Street { Name = street.Name, CityId = city.Id });
            _database.Save();
        }

        public StreetDTO GetStreetById(int streetId)
        {
            var result = _database.Streets.Select().Include(p => p.City).Where(p => p.Id == streetId).ToList();
            return result.Count > 0 ? new StreetDTO(result[0]) : null;
        }

        public StreetDTO GetStreetByNameInCity(string cityName, string name)
        {

            var result = _database.Streets.Select().Include(p => p.City).Where(p => p.Name == name && p.City.Name == cityName).ToList();
            return result.Count > 0 ? new StreetDTO(result[0]) : null;
        }

        public IEnumerable<StreetDTO> GetStreets(int cityId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Street, StreetDTO>()
                                                .ForMember("CityName", opt => opt.MapFrom(c => c.City.Name))).CreateMapper();
            var l = _database.Streets.Select().Include(p => p.City).Where(p => p.CityId == cityId).ToList();
            return mapper.Map<List<Street>, List<StreetDTO>>(l);
        }

        public void CreateBuilding(BuildingDTO building)
        {
            /*var result = _database.Streets.Select().Include(s => s.City)
                .Where(s => s.Name == building.StreetName && s.City.Name == building.CityName)
                .ToList();
            var street = result[0];*/
            var street = _database.Streets.FindById(building.StreetId);
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

        public IEnumerable<BuildingDTO> GetBuildingsOnStreet(int streetId)
        {
            List<BuildingDTO> result = new List<BuildingDTO>();
            foreach (var building in _database.Buildings.Select().Include(s => s.Street).Include(s => s.Street.City).Where(p => p.StreetId == streetId).ToList())
            {
                result.Add(new BuildingDTO(building));
            }
            return result;
        }

        public void DeleteCity(int id)
        {
            var city = _database.Cities.FindById(id);
            _database.Cities.Remove(city);
            _database.Save();
        }

        public void DeleteStreet(int id)
        {
            var street = _database.Streets.FindById(id);
            _database.Streets.Remove(street);
            _database.Save();
        }

        public void DeleteBuilding(int id)
        {
            var building = _database.Buildings.FindById(id);
            _database.Buildings.Remove(building);
            _database.Save();
        }
    }
}
