using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IRentService
    {
        void CreateRent(RentDTO rent);
        void StopRent(int id);
        RentDTO GetRentById(int id);
        List<RentDTO> GetAllActiveRentsForStore(int rentStoreId);
        List<RentDTO> GetAllEndedRentsForStore(int rentStoreId);
        List<RentDTO> GetAllActiveRentsForManager(int managerId);
        List<RentDTO> GetAllEndedRentsForManager(int managerId);
        List<RentDTO> GetAllActiveRentsForClient(int clientId);
        List<RentDTO> GetAllEndedRentsForClient(int clientId);
    }
}
