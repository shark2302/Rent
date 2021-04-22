using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IAddressService
    {
        void CreateCity(CityDTO city);
        CityDTO GetCityByName(string name);
        IEnumerable<CityDTO> GetCities();
        void CreateStreet(StreetDTO street);
        IEnumerable<StreetDTO> GetStreets();
        StreetDTO GetStreetByNameInCity(string city, string name);
        void CreateBuilding(BuildingDTO building);
        IEnumerable<BuildingDTO> GetBuildings();
        BuildingDTO GetBuildingByNumberInStreet(string city, string name, int number);
    }
}
