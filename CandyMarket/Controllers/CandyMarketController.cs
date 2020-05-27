using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyMarket.DataAccess;
using CandyMarket.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandyMarket.Controllers
{
    [Route("api/candy")]
    [ApiController]
    public class CandyMarketController : ControllerBase
    {
        // GET: api/candy
        [HttpGet]
        public IActionResult GetAllCandy()
        {
            var repo = new CandyMarketRepository();

            var AllCandy = repo.GetAllCandy();

            return Ok(AllCandy);
        }

        // GET api/candy/1
        [HttpGet("{id}")]
        public IActionResult GetCandyByUser(int id)
        {
            var repo = new CandyMarketRepository();
            //var user = getUserById(id);
            var candy = repo.GetCandyByUser(id);
            //user == null
            if (candy == null) return NotFound("No candy or user found."); // Add user to the IF statement.

            return Ok(candy);
        }

        // PUT api/candy/eat/1/4
        [HttpPut("eat/{userId}/{candyId}")]
        public IActionResult EatCandy(int userId, int candyId)
        {
            var repo = new CandyMarketRepository();
            var userCandy = repo.EatCandy(userId, candyId);
            return Ok(userCandy);
        }

        //PUT api/candy/eat/random/1/Chocolate
        [HttpPut("eat/random/{userId}/{flavorCategory}")]
        public IActionResult userFlavorCategory(int userId, string flavorCategory)
        {
            var repo = new CandyMarketRepository();
            var userFlavor = repo.EatRandomCandyByFlavor(userId, flavorCategory);
            return Ok(userFlavor);
        }

        //PUT api/candy/trade/1/2
        [HttpPut("trade/{userId1}/{userId2}")]
        public IActionResult TradeCandy(int userId1, int userId2)
        {
            var repo = new CandyMarketRepository();
            var result = repo.TradeCandy(userId1, userId2);
            return Ok(result);
        }

        // DELETE api/<CandyMarketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
