using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? RentStoreId { get; set; }
        public virtual RentStore RentStore { get; set; }
    }
}
