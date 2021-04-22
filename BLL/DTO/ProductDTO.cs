using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class ProductDTO
    {
        public string Name { get; set; }

        public ProductDTO(Product product)
        {
            Name = product.Name;
        }

        public ProductDTO()
        {
        }
    }
}
