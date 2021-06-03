using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ProductDTO(Product product)
        {
            Name = product.Name;
            Id = product.Id;
        }

        public ProductDTO()
        {
        }
    }
}
