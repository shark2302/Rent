using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class RentStore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BuildingId { get; set; }
        public virtual Building Building { get; set; }
        public virtual List<ProductPrice> Products { get; set; }
    }
}
