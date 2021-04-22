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
    }
}
