﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Models.ViewModels
{
    public class RandomCandy
    {
        public int CandyId { get; set; }
        public int UserCandyId { get; set; }
        public string FlavorCategory { get; set; }
        public DateTime DateReceived { get; set; }
    }
}
