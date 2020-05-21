using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Models
{
    public class Candy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FlavorCategory { get; set; }
        public string Manufacturer { get; set; }
    }
}
