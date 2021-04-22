using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
    public class ManagerDTO
    {
        public string Name { get; set; }
        public string RentStoreName { get; set; }

        public ManagerDTO(Manager manager)
        {
            Name = manager.Name;
            RentStoreName = manager.RentStore.Name;
        }

        public ManagerDTO()
        {
        }
    }
}
