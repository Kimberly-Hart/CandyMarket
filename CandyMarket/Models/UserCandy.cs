using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Models
{
    public class UserCandy
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CandyId { get; set; }
        public DateTime DateReceived { get; set; }
        public bool IsEaten { get; set; }
    }
}
