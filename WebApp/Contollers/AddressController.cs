using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

public class Address : Controller
{
    private IAddressService _addressService;
    private ILogger<Address> _logger;

    public Address(IAddressService addressService, ILogger<Address> logger)
    {
        _logger = logger;
        this._addressService = addressService;
    }

    public IActionResult GetAllCities()
    {
        _logger.LogInformation("All cities was got");
        return View(_addressService.GetCities());
    }

    [HttpPost]
    public IActionResult GetAllCities(CityDTO city)
    {
        _addressService.CreateCity(city);
        _logger.LogInformation("New city was added " + city.Name);
        return View(_addressService.GetCities());
    }

    public IActionResult DeleteCity(int id)
    {
        _addressService.DeleteCity(id);
        _logger.LogInformation("City was deleted (id=" + id + ")");
        return RedirectToAction("GetAllCities");
    }

    [ActionName("GetAllStreetsInCity")]
    public IActionResult GetAllStreetsInCity(int cityId)
    {
        ViewBag.Message = _addressService.GetCityById(cityId);
        _logger.LogInformation("All streets for city get (id=" + cityId + ")");
        return View(_addressService.GetStreets(cityId));
    }

    [HttpPost]
    public IActionResult GetAllStreetsInCity(StreetDTO street)
    {
        _addressService.CreateStreet(street);
        _logger.LogInformation("New street was added " + street.Name);
        return GetAllStreetsInCity(street.CityId);
    }

    public IActionResult DeleteStreet(int id, int cityId)
    {
        _addressService.DeleteStreet(id);
        return RedirectToAction("GetAllStreetsInCity", new { cityId });
    }

    public IActionResult GetAllBuildingsOnStreet(int streetId)
    {
        ViewBag.Message = _addressService.GetStreetById(streetId);
        return View(_addressService.GetBuildingsOnStreet(streetId));
    }

    [HttpPost]
    public IActionResult GetAllBuildingsOnStreet(BuildingDTO building)
    {
        _addressService.CreateBuilding(building);
        return GetAllBuildingsOnStreet(building.StreetId);
    }

    public IActionResult DeleteBuilding(int id, int streetId)
    {
        _addressService.DeleteBuilding(id);
        return RedirectToAction("GetAllBuildingsOnStreet", new { streetId });
    }


}