using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.InteropServices;

public class RentStore : Controller
{
    private IRentStoreService _rentStoreService;
    private IProductPriceService _productPriceService;
    private IProductService _productService;
    private IManagerService _managerService;
    private IClientService _clientService;
    private IRentService _rentService;

    public RentStore(IRentStoreService rentStoreService, IProductPriceService productPriceService, 
        IProductService productService, IManagerService managerService, IClientService clientService, IRentService rentService)
    {
        _rentStoreService = rentStoreService;
        _productPriceService = productPriceService;
        _productService = productService;
        _managerService = managerService;
        _clientService = clientService;
        _rentService = rentService;
    }

    public IActionResult GetAllRentStores()
    {
        return View(_rentStoreService.GetRentStores());
    }
    [HttpPost]
    public IActionResult GetAllRentStores(string name, string city, string street, int number)
    {
        try
        {
            _rentStoreService.CreateRentStore(new RentStoreDTO { Name = name, Building = new BuildingDTO { Number = number, CityName = city, StreetName = street } });
        }
        catch (NullReferenceException e)
        {
            ViewBag.Message = "Ошибка в создании адреса";
        }
        return View(_rentStoreService.GetRentStores());
    }

    public IActionResult ControlRentStore(int id)
    {
        return View(_rentStoreService.GetRentStoreById(id));
    }

    public IActionResult DeleteRentStore(int id)
    {
        _rentStoreService.DeleteRentStore(id);
        return Redirect("GetAllRentStores");
    }

    public IActionResult GetAllRentStoreProducts(int id)
    {
        var store = _rentStoreService.GetRentStoreById(id);
        ViewBag.Store = store;
        ViewBag.Products = _productService.GetProducts();
        return View(store.Products);
    }

    [HttpPost]
    public IActionResult GetAllRentStoreProducts(ProductPriceDTO product)
    {
        try
        {
            _productPriceService.CreateProductPrice(product);
        }
        catch (NullReferenceException e)
        {
            ViewBag.Message = "Такого продукта не существует";
        }
        return GetAllRentStoreProducts(product.RentStoreId);
    }

    public IActionResult GetAllRentStoreManagers(int rentStoreId)
    {
        var store = _rentStoreService.GetRentStoreById(rentStoreId);
        ViewBag.Store = store;
        return View(_managerService.GetAllRentStoreManagers(rentStoreId));
    }

    [HttpPost]
    public IActionResult GetAllRentStoreManagers(ManagerDTO manager)
    {
        _managerService.CreateManager(manager);
        return GetAllRentStoreManagers(manager.RentStoreId);
    }

    public IActionResult DeleteManager(int id, int rentStoreId)
    {
        _managerService.DeleteManager(id);
        return RedirectToAction("GetAllRentStoreManagers", new { rentStoreId });
    }

    public IActionResult CreateRent(int rentStoreId)
    {
        var store = _rentStoreService.GetRentStoreById(rentStoreId);
        ViewBag.Store = store;
        ViewBag.Clients = _clientService.GetClients();
        ViewBag.Managers = _managerService.GetAllRentStoreManagers(rentStoreId);
        ViewBag.Products = _productPriceService.GetAllForStore(rentStoreId);
        return View();
    }
    [HttpPost]
    public IActionResult CreateRent(string productName, string clientName, string managerName, int rentStoreId)
    {
        var product = _productPriceService.GetByProductAndStore(productName, rentStoreId);
        _rentService.CreateRent(new RentDTO { ClientName = clientName, ManagerName = managerName,
            RentStoreId = rentStoreId, Product = product});
        TempData["RentCreated"] = true;
        return RedirectToAction("ControlRentStore", new { id = rentStoreId });
    }

    public IActionResult GetAllActiveRents(int rentStoreId)
    {
        var store = _rentStoreService.GetRentStoreById(rentStoreId);
        ViewBag.Store = store;
        return View(_rentService.GetAllActiveRentsForStore(rentStoreId));
    }

    public IActionResult StopRent(int rentId, int rentStoreId)
    {
        _rentService.StopRent(rentId);
        return RedirectToAction("GetAllActiveRents", new { rentStoreId });
    }

    public IActionResult GetAllEndedRents(int rentStoreId)
    {
        var store = _rentStoreService.GetRentStoreById(rentStoreId);
        ViewBag.Store = store;
        return View(_rentService.GetAllEndedRentsForStore(rentStoreId));
    }

    public IActionResult GetAllActiveRentsForManager(int managerId)
    {
        var manager = _managerService.GetManagerById(managerId);
        ViewBag.Manager = manager;
        return View("GetAllActiveRents", _rentService.GetAllActiveRentsForManager(managerId));
    }

    public IActionResult GetAllEndedRentsForManager(int managerId)
    {
        var manager = _managerService.GetManagerById(managerId);
        ViewBag.Manager = manager;
        return View("GetAllEndedRents", _rentService.GetAllEndedRentsForManager(managerId));
    }
}
