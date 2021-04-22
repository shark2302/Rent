using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class Building
    {
        public int Id { get; set; }
        public int Number { get; set; }
        [Required]
        public int StreetId { get; set; }
        public virtual Street Street { get; set; }
    }
}
