using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IRentStoreService
    {
        void CreateRentStore(RentStoreDTO rentStoreDTO);
        RentStoreDTO GetRentStoreById(int id);
        RentStoreDTO GetRentStoreByNameAndAdress(string name, BuildingDTO buildingDTO);
        IEnumerable<RentStoreDTO> GetRentStores();
        void DeleteRentStore(int id);
    }
}
