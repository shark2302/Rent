using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class ProductPrice
    {
        [Key]
        public int ProductId { get; set; }
        public int RentStoreId { get; set; }
        public int Price { get; set; }
        [Required]
        public virtual Product Product { get; set; }
        public virtual RentStore RentStore { get; set; }
    }
}
