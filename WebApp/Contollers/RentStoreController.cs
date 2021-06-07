using BLL.DTO;
using BLL.Interfaces;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

public class RentStore : Controller
{
    private IRentStoreService _rentStoreService;
    private IProductPriceService _productPriceService;
    private IProductService _productService;
    private IManagerService _managerService;
    private IClientService _clientService;
    private IRentService _rentService;

    private ILogger<RentStore> _logger;

    public RentStore(IRentStoreService rentStoreService, IProductPriceService productPriceService, 
        IProductService productService, IManagerService managerService, IClientService clientService, IRentService rentService,
        ILogger<RentStore> logger)
    {
        _logger = logger;
        _rentStoreService = rentStoreService;
        _productPriceService = productPriceService;
        _productService = productService;
        _managerService = managerService;
        _clientService = clientService;
        _rentService = rentService;
    }

    public IActionResult GetAllRentStores()
    {
        _logger.LogInformation("All rent stores was get");
        return View(_rentStoreService.GetRentStores().OrderBy(p => p.Name));
    }
    [HttpPost]
    public IActionResult GetAllRentStores(string name, string city, string street, int number)
    {
        try
        {
            _rentStoreService.CreateRentStore(new RentStoreDTO { Name = name, Building = new BuildingDTO { Number = number, CityName = city, StreetName = street } });
            _logger.LogInformation("Rent store " + name + " was created");
        }
        catch (NullReferenceException e)
        {
            ViewBag.Message = "Ошибка в создании адреса";
            _logger.LogError("Error creating rent store");
        }
        return View(_rentStoreService.GetRentStores().OrderBy(p => p.Name));
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
        ViewBag.Products = _productService.GetProducts().OrderBy(p => p.Name);
        _logger.LogInformation("All " + store.Name + "\'s products was get");
        return View(store.Products.OrderBy(p => p.ProductName));
    }

    [HttpPost]
    public IActionResult GetAllRentStoreProducts(ProductPriceDTO product)
    {
        try
        {
            _productPriceService.CreateProductPrice(product);
            _logger.LogInformation("Product " + product.ProductName + " was created");
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
        _logger.LogInformation("All " + store.Name + "\'s managers was get");
        return View(_managerService.GetAllRentStoreManagers(rentStoreId).OrderBy(p => p.Name));
    }

    [HttpPost]
    public IActionResult GetAllRentStoreManagers(ManagerDTO manager)
    {
        _managerService.CreateManager(manager);
        _logger.LogInformation("Manager " + manager.Name + " was created");
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
        _logger.LogInformation("New rent was created");
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

    [HttpGet]
    public IActionResult GetAllEndedRents(int rentStoreId, bool download, int clientId, int managerId, string productName, DateTime from, DateTime to)
    {
        ViewBag.Clients = _clientService.GetClients();
        ViewBag.Managers = _managerService.GetAllRentStoreManagers(rentStoreId);
        ViewBag.Products = _productPriceService.GetAllForStore(rentStoreId);
        var store = _rentStoreService.GetRentStoreById(rentStoreId);
        ViewBag.Store = store;
        var rents = _rentService.GetAllEndedRentsForStore(rentStoreId);
        if (clientId != 0 || managerId != 0 || !String.IsNullOrEmpty(productName) || from != DateTime.MinValue || to != DateTime.MinValue)
        {
            _logger.LogInformation("Getting filtered rents");
            rents = _rentService.GetFilteredRents(rentStoreId, clientId, managerId, productName, from, to);
            string queryResultString = String.Empty;
            if (clientId != 0)
                queryResultString += "Клиент: " + _clientService.GetClient(clientId).Name + "\n";
            if (managerId != 0)
                queryResultString += "Менеджер: " + _managerService.GetManagerById(managerId).Name + "\n";
            if (!String.IsNullOrEmpty(productName))
                queryResultString += "Продукт: " + productName + "\n";
            if (from != DateTime.MinValue)
                queryResultString += "Дата от: " + from + "\n";
            if (to != DateTime.MinValue)
                queryResultString += "Дата до: " + to + "\n";
            queryResultString += "Итоговая сумма по запросу: " + Math.Round(CountTotalSum(rents), 2) + "\n";

            ViewBag.QueryResultString = queryResultString;
        }
        if (download)
        { 
            using var memStream = new MemoryStream();
            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Rents");
            worksheet.Cell("A1").Value = "Rent Id";
            worksheet.Cell("B1").Value = "Product Name";
            worksheet.Cell("C1").Value = "Client Name";
            worksheet.Cell("D1").Value = "Manager Name";
            worksheet.Cell("E1").Value = "Start time";
            worksheet.Cell("F1").Value = "End time";
            worksheet.Cell("G1").Value = "Check";
           
            int row = 1;
            foreach (var rent in rents)
            {
                var rowObj = worksheet.Row(++row);
                rowObj.Cell(1).Value = rent.Id;
                rowObj.Cell(2).Value = rent.Product.ProductName;
                rowObj.Cell(3).Value = rent.ClientName;
                rowObj.Cell(4).Value = rent.ManagerName;
                rowObj.Cell(5).Value = rent.StartTime;
                rowObj.Cell(6).Value = rent.EndTime;
                rowObj.Cell(7).Value = rent.Check;

            }
            workbook.SaveAs(memStream);
            _logger.LogInformation("Downloading file");
            return File(memStream.ToArray(), "application/force-download", "Rents.xlsx");
        }
        
        return View(rents);
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

    [HttpPost]
    public IActionResult GetAllEndedRents(int rentStoreId, int clientId, int managerId, string productName, DateTime from, DateTime to)
    {
        ViewBag.Clients = _clientService.GetClients();
        ViewBag.Managers = _managerService.GetAllRentStoreManagers(rentStoreId);
        ViewBag.Products = _productPriceService.GetAllForStore(rentStoreId);
        var store = _rentStoreService.GetRentStoreById(rentStoreId);
        ViewBag.Store = store;
        var res = _rentService.GetFilteredRents(rentStoreId, clientId, managerId, productName, from, to);
        _logger.LogInformation("Getting filtered rents");
        string queryResultString = String.Empty;
        if (clientId != 0)
            queryResultString += "Клиент: " + _clientService.GetClient(clientId).Name + "\n";
        if (managerId != 0)
            queryResultString += "Менеджер: " + _managerService.GetManagerById(managerId).Name + "\n";
        if (!String.IsNullOrEmpty(productName))
            queryResultString += "Продукт: " + productName + "\n";
        if (from != DateTime.MinValue)
            queryResultString += "Дата от: " + from + "\n";
        if (to != DateTime.MinValue)
            queryResultString += "Дата до: " + to + "\n";
        queryResultString += "Итоговая сумма по запросу: " + Math.Round(CountTotalSum(res), 2) + "\n";

        ViewBag.QueryResultString = queryResultString;
        return View("GetAllEndedRents", res);
    }


    [NonAction]
    private double CountTotalSum(List<RentDTO> rents)
    {
        double res = 0;
        foreach(var rent in rents)
        {
            res += rent.Check;
        }
        return res;
    }
}
