using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
    public class BuildingDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string StreetName { get; set; }
        public string CityName { get; set; }

        public int StreetId { get; set; }

        public BuildingDTO(Building building)
        {
            Id = building.Id;
            Number = building.Number;
            StreetId = building.StreetId;
            StreetName = building.Street.Name;
            CityName = building.Street.City.Name;
        }
        public BuildingDTO()
        {
          
        }

    }
}
