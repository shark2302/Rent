using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

public class Address : Controller
{
    private IAddressService _addressService;

    public Address(IAddressService addressService)
    {
        this._addressService = addressService;
    }

    public IActionResult GetAllCities()
    {
        return View(_addressService.GetCities());
    }

    [HttpPost]
    public IActionResult GetAllCities(CityDTO city)
    {
        _addressService.CreateCity(city);
        return View(_addressService.GetCities());
    }

    public IActionResult DeleteCity(int id)
    {
        _addressService.DeleteCity(id);
        return RedirectToAction("GetAllCities");
    }

    [ActionName("GetAllStreetsInCity")]
    public IActionResult GetAllStreetsInCity(int cityId)
    {
        ViewBag.Message = _addressService.GetCityById(cityId);
        return View(_addressService.GetStreets(cityId));
    }

    [HttpPost]
    public IActionResult GetAllStreetsInCity(StreetDTO street)
    {
        _addressService.CreateStreet(street);
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