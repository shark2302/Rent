using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.InteropServices;

public class Client : Controller
{
    private IClientService _clientService;

    public Client(IClientService clientService)
    {
        _clientService = clientService;
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
        return View(_clientService.GetClient(id));
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
}
