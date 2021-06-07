using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Runtime.InteropServices;

public class Client : Controller
{
    private IClientService _clientService;
    private IRentService _rentService;
    private ILogger<Client> _logger;

    public Client(IClientService clientService, IRentService rentService, ILogger<Client> logger)
    {
        _logger = logger; 
        _clientService = clientService;
        _rentService = rentService;
    }

    public IActionResult GetAllClients()
    {
        _logger.LogInformation("All clients was get");
        return View(_clientService.GetClients().OrderBy(p => p.Name));
    }
    [HttpPost]
    public IActionResult GetAllClients(string name, string city, string street, int number)
    {
        try
        {
            _clientService.CreateClient(new ClientDTO { Name = name, Building = new BuildingDTO { Number = number, CityName = city, StreetName = street } });
            _logger.LogInformation("Новый клиент создан " + name);
        }catch (NullReferenceException e)
        {
            ViewBag.Message = "Ошибка в создании адреса";
            _logger.LogError("Error creating user");
        }
            return View(_clientService.GetClients().OrderBy(p => p.Name));
    }

    public IActionResult UpdateClient(int id)
    {
        ViewBag.Client = _clientService.GetClient(id);
        
        return View("GetAllClients", _clientService.GetClients().OrderBy(p => p.Name));
    }

    [HttpPost]
    public IActionResult UpdateClient(int id, string name, string city, string street, int number)
    {
        try
        {
            _clientService.UpdateClient(id, new ClientDTO { Name = name, Building = new BuildingDTO { Number = number, CityName = city, StreetName = street } });
            _logger.LogInformation("Клиент " + name + " обновлен");
        }
        catch (NullReferenceException e)
        {
            ViewBag.Message = "Ошибка в создании адреса";
            _logger.LogError("Error updating user");
            return View("GetAllClients", _clientService.GetClients().OrderBy(p => p.Name));
        }
        return Redirect("GetAllClients");
    }

    public IActionResult DeleteClient(int id)
    {
        _clientService.DeleteClient(id);
        return Redirect("GetAllClients");
    }

    public IActionResult GetAllActiveRentsForClient(int clientId)
    {
        var client = _clientService.GetClient(clientId);
        ViewBag.Client = client;
        return View("Views/RentStore/GetAllActiveRents.cshtml", _rentService.GetAllActiveRentsForClient(clientId));
    }

    public IActionResult GetAllEndedRentsForClient(int clientId)
    {
        var client = _clientService.GetClient(clientId);
        ViewBag.Client = client;
        return View("Views/RentStore/GetAllEndedRents.cshtml", _rentService.GetAllEndedRentsForClient(clientId));
    }
}
