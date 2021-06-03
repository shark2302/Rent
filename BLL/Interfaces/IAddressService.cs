using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IAddressService
    {
        void CreateCity(CityDTO city);
        CityDTO GetCityById(int cityId);
        CityDTO GetCityByName(string name);
        IEnumerable<CityDTO> GetCities();
        void CreateStreet(StreetDTO street);
        StreetDTO GetStreetById(int streetId);
        IEnumerable<StreetDTO> GetStreets(int cityId);
        StreetDTO GetStreetByNameInCity(string city, string name);
        void CreateBuilding(BuildingDTO building);
        IEnumerable<BuildingDTO> GetBuildings();
        IEnumerable<BuildingDTO> GetBuildingsOnStreet(int streetId);
        BuildingDTO GetBuildingByNumberInStreet(string city, string name, int number);
        void DeleteCity(int id);
        void DeleteStreet(int id);
        void DeleteBuilding(int id);
    }
}
