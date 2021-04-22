using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IManagerService
    {
        void CreateManager(ManagerDTO managerDTO);
        ManagerDTO FindByNameAndRentStore(string name, string rentStoreName);
    }
}
