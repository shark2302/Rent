using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class Rent
    {
        public int Id { get; set; }
        public int ClientId{get;set;}
        public int ProductId{ get; set; }
        public int RentStoreId{ get; set; }
        public int ManagerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public virtual Client Client { get; set; }
        public virtual Product Product { get; set; }
        public virtual RentStore RentStore { get; set; }
        public virtual Manager Manager { get; set; }

    }
}
