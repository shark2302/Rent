using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        _logger.LogInformation("All products get");
        return View(_productService.GetProducts());
    }

    [HttpPost]
    public IActionResult GetAllProducts(ProductDTO product)
    {
        _productService.CreateProduct(product);
        _logger.LogInformation("New product " + product.Name + " was created");
        return View(_productService.GetProducts());
    }

    public IActionResult DeleteProduct(int id)
    {
        _productService.DeleteProduct(id);
        return Redirect("GetAllProducts");
    }
}
