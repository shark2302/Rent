using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
    public class StreetDTO
    {
        public string Name { get; set; }
        public string CityName { get; set; }

        public StreetDTO(Street street)
        {
            Name = street.Name;
            CityName = street.City.Name;
        }

        public StreetDTO()
        {
          
        }

    }
}
