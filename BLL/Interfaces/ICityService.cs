using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICityService
    {
        void CreateCity(CityDTO city);
        CityDTO GetCityByName(string name);
        IEnumerable<CityDTO> GetCities();
    }
}
