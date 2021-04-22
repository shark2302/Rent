using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
    public class RentDTO
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ClientName { get; set; }
        public ProductPriceDTO Product { get; set; }
        public string RentStoreName { get; set; }
        public string ManagerName { get; set; }


        public float Check { get
            {
                if (EndTime != null)
                    return (EndTime.Hour - StartTime.Hour) * Product.Price;
                return -1;
            } }

        public RentDTO(Rent rent, ProductPrice productPrice)
        {
            Id = rent.Id;
            StartTime = rent.StartTime;
            EndTime = rent.EndTime;
            ClientName = rent.Client.Name;
            Product = new ProductPriceDTO(productPrice);
            RentStoreName = rent.RentStore.Name;
            ManagerName = rent.Manager.Name;
        }

        public RentDTO()
        {
        }
    }
}
