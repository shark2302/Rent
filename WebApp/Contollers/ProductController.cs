using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

public class Product : Controller
{
    private IProductService _productService;
    private ILogger<Product> _logger;

    public Product(IProductService productService, ILogger<Product> logger)
    {
        _logger = logger;
        _productService = productService;
    }

    public IActionResult GetAllProducts()
    {
        List<ProductDTO> products = _productService.GetProducts();
        var sorted = products.OrderBy(p => p.Name).ToList();
        return View(sorted);
    }

    [HttpPost]
    public IActionResult GetAllProducts(ProductDTO product)
    {
        _productService.CreateProduct(product);
        _logger.LogInformation("New product " + product.Name + " was created");
        return View(_productService.GetProducts().OrderBy(p => p.Name));
    }

    public IActionResult DeleteProduct(int id)
    {
        _productService.DeleteProduct(id);
        return Redirect("GetAllProducts");
    }
}
