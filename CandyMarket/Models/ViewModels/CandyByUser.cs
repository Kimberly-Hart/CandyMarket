﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Models.ViewModels
{
    public class CandyByUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string FlavorCategory { get; set; }
        public DateTime DateReceived { get; set; }
    }
}
