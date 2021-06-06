using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.InteropServices;

public class Client : Controller
{
    private IClientService _clientService;
    private IRentService _rentService;

    public Client(IClientService clientService, IRentService rentService)
    {
        _clientService = clientService;
        _rentService = rentService;
    }

    public IActionResult GetAllClients()
    {
        return View(_clientService.GetClients());
    }
    [HttpPost]
    public IActionResult GetAllClients(string name, string city, string street, int number)
    {
        try
        {
            _clientService.CreateClient(new ClientDTO { Name = name, Building = new BuildingDTO { Number = number, CityName = city, StreetName = street } });
        }catch (NullReferenceException e)
        {
            ViewBag.Message = "Ошибка в создании адреса";
        }
            return View(_clientService.GetClients());
    }

    public IActionResult UpdateClient(int id)
    {
        ViewBag.Client = _clientService.GetClient(id);
        return View("GetAllClients", _clientService.GetClients());
    }

    [HttpPost]
    public IActionResult UpdateClient(int id, string name, string city, string street, int number)
    {
        try
        {
            _clientService.UpdateClient(id, new ClientDTO { Name = name, Building = new BuildingDTO { Number = number, CityName = city, StreetName = street } });
        }
        catch (NullReferenceException e)
        {
            ViewBag.Message = "Ошибка в создании адреса";
            return View(_clientService.GetClient(id));
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
