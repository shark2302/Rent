using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IBuildingService
    {
        void CreateBuilding(BuildingDTO building);
        IEnumerable<BuildingDTO> GetBuildings();
        BuildingDTO GetBuildingByNumberInStreet(string city, string name, int number);
    }
}
