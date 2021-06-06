using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class ManagerService : IManagerService
    {

        private IUnitOfWork _database;
        public ManagerService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }
        public void CreateManager(ManagerDTO managerDTO)
        {
            var res = _database.RentStores.Get(p => p.Name == managerDTO.RentStoreName).GetEnumerator();
            var rentStore = _database.RentStores.FindById(managerDTO.RentStoreId);
            _database.Managers.Create(new Manager { Name = managerDTO.Name, RentStoreId = rentStore.Id });
            _database.Save();
        }

        public List<ManagerDTO> GetAllRentStoreManagers(int rentStoreId)
        {
            List<ManagerDTO> res = new List<ManagerDTO>();
            foreach(var manager in _database.Managers.Select().Include(p => p.RentStore)
               .Where(p => p.RentStoreId == rentStoreId).ToList())
            {
                res.Add(new ManagerDTO(manager));
            }
            return res;
        }

        public ManagerDTO FindByNameAndRentStore(string name, string rentStoreName)
        {
            var manager = _database.Managers.Select().Include(p => p.RentStore)
               .Where(p => p.RentStore.Name == rentStoreName && p.Name == name).ToList()[0];

            return manager != null ? new ManagerDTO(manager) : null;
        }

        public void DeleteManager(int id)
        {
            _database.Managers.Remove(_database.Managers.FindById(id));
            _database.Save();
        }

        public ManagerDTO GetManagerById(int id)
        {
            return new ManagerDTO(_database.Managers.FindById(id));
        }
    }
}
