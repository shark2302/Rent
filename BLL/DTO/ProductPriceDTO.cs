using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
    public class ProductPriceDTO
    {
        public int Price { get; set; }
        public string ProductName { get; set; }
        public string RentStoreName { get; set; }

        public ProductPriceDTO()
        {
        }

        public ProductPriceDTO(ProductPrice price) 
        {
            Price = price.Price;
            ProductName = price.Product.Name;
            RentStoreName = price.RentStore.Name;
        }
    }
}
