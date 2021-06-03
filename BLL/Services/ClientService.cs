using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using DAL.Entities;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BLL.Services
{
    public class ClientService : IClientService
    {
        private IUnitOfWork _database;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public void CreateClient(ClientDTO client)
        {
            var building = _database.Buildings.Select().Include(s => s.Street).Include(s => s.Street.City)
                .Where(s => s.Street.Name == client.Building.StreetName && s.Street.City.Name == client.Building.CityName
                && s.Number == client.Building.Number)
                .ToList();
            if (building.Count > 0)
            {
                _database.Clients.Create(new Client() { Name = client.Name, BuildingId = building[0].Id });
                _database.Save();
            }
            else
                throw new NullReferenceException();
           
        }

        public ClientDTO GetClient(int? id)
        {
            var client = _database.Clients.FindById(id.Value);
            var building = _database.Buildings.FindById(client.BuildingId);
            return client != null ? new ClientDTO {Id = client.Id, Name = client.Name, Building = new BuildingDTO(building), BuildingId = building.Id } : null;
        }

        public IEnumerable<ClientDTO> GetClients()
        {
            var result = new List<ClientDTO>();
            foreach(var client in _database.Clients.Select().Include(s => s.Building).
                Include(s => s.Building.Street).Include(s => s.Building.Street.City).ToList())
            {
                result.Add(new ClientDTO { Building = new BuildingDTO(client.Building), Name = client.Name, Id = client.Id, BuildingId = client.BuildingId });
            }
            return result;
        }

        public void UpdateClient(int id, ClientDTO clientDTO)
        {
            var client = _database.Clients.FindById(id);
            client.Name = clientDTO.Name;
            
            var building = _database.Buildings.Select().Include(s => s.Street).Include(s => s.Street.City)
               .Where(s => s.Street.Name == clientDTO.Building.StreetName && s.Street.City.Name == clientDTO.Building.CityName
               && s.Number == clientDTO.Building.Number)
               .ToList();
            if (building.Count > 0)
                client.BuildingId = building[0].Id;
            else
                throw new NullReferenceException();
            _database.Clients.Update(client);
            _database.Save();
        }


        public void DeleteClient(int id)
        {
            _database.Clients.Remove(_database.Clients.FindById(id));
            _database.Save();
        }

        
    }
}
