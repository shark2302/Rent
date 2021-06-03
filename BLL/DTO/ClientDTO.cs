using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BuildingDTO Building { get; set; }
        public int BuildingId { get; set; }
    }
}
