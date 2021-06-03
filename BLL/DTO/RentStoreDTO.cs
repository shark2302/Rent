using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class RentStoreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BuildingDTO Building { get; set; }
        public List<ProductPriceDTO> Products { get; set; }

        public RentStoreDTO()
        {
        }
        
        public RentStoreDTO(RentStore rentStore, List<ProductPriceDTO> products) 
        {
            Id = rentStore.Id;
            Name = rentStore.Name;
            Building = new BuildingDTO(rentStore.Building);
            Products = products;
        }
        
        

    }
}
