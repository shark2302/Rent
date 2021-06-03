using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
    public class StreetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public int CityId { get; set; }

        public StreetDTO(Street street)
        {
            Id = street.Id;
            Name = street.Name;
            CityName = street.City.Name;
            CityId = street.CityId;
        }

        public StreetDTO()
        {
          
        }

    }
}
