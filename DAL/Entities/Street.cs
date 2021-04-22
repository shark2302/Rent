using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class Street
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public int CityId { get; set; }
        public virtual City City { get; set; }
    }
}
