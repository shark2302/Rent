using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

public class Product : Controller
{
    private IProductService _productService;

    public Product(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult GetAllProducts()
    {
        return View(_productService.GetProducts());
    }

    [HttpPost]
    public IActionResult GetAllProducts(ProductDTO product)
    {
        _productService.CreateProduct(product);
        return View(_productService.GetProducts());
    }

    public IActionResult DeleteProduct(int id)
    {
        _productService.DeleteProduct(id);
        return Redirect("GetAllProducts");
    }
}
