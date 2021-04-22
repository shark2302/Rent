using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace BLL.Services
{
    public class CityService : ICityService
    {

        private IUnitOfWork _database;

        public CityService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public void CreateCity(CityDTO city)
        {
            _database.Cities.Create(new City { Name = city.Name });
            _database.Save();
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
    }
}
