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
    public class StreetService : IStreetService
    {

        private IUnitOfWork _database;
        
        public StreetService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }
        public void CreateStreet(StreetDTO street)
        {
            var result = _database.Cities.Get(c=> c.Name == street.CityName).GetEnumerator();
            var city = result.MoveNext() ? result.Current : null;
            _database.Streets.Create(new Street {Name = street.Name, CityId = city.Id});
            _database.Save();
        }

        public StreetDTO GetStreetByNameInCity(string cityName, string name)
        {
           
            var result = _database.Streets.Select().Include(p => p.City).Where(p => p.Name == name && p.City.Name == cityName).ToList();
            return result.Count > 0 ? new StreetDTO(result[0]) : null;
            /*var res = _database.Streets.Get(c => c.Name == name && c.City.Name == cityName).GetEnumerator();
            return res.MoveNext() ? new StreetDTO(res.Current) : null;*/
        }

        public IEnumerable<StreetDTO> GetStreets()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Street, StreetDTO>()
                                                .ForMember("CityName", opt => opt.MapFrom(c => c.City.Name))).CreateMapper();
            return mapper.Map<List<Street>, List<StreetDTO>>(_database.Streets.Select().Include(p => p.City).ToList());
        }
    }
}
