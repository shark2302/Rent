using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        void CreateProduct(ProductDTO productDTO);
        ProductDTO FindByName(string name);
        List<ProductDTO> GetProducts();
        void DeleteProduct(int id);
    }
}
