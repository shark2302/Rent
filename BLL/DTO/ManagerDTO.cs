using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
    public class ManagerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RentStoreId { get; set; }
        public string RentStoreName { get; set; }

        public ManagerDTO(Manager manager)
        {
            Id = manager.Id;
            RentStoreId = (int)manager.RentStoreId;
            Name = manager.Name;
            RentStoreName = manager.RentStore.Name;
        }

        public ManagerDTO()
        {
        }
    }
}
