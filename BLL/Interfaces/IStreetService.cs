using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IStreetService
    {
        void CreateStreet(StreetDTO street);
        IEnumerable<StreetDTO> GetStreets();
        StreetDTO GetStreetByNameInCity(string city, string name);
    }
}
