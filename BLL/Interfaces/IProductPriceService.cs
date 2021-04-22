using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IProductPriceService
    {
        void CreateProductPrice(ProductPriceDTO pruductPriceDTO);
        ProductPriceDTO GetByProductAndStore(string productName, string rentStoreName);
        IEnumerable<ProductPriceDTO> GetAllForStore(string storeName);
    }
}
