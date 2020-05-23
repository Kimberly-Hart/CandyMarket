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

        // POST api/<CandyMarketController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/candy/eat/userCandyId
        [HttpPut("eat/{userId}/{candyId}")]
        public IActionResult EatCandy(int userId, int candyId)
        {
            var repo = new CandyMarketRepository();
            var userCandy = repo.EatCandy(userId, candyId);
            return Ok(userCandy);
        }

        //PUT api/candy/eat/userId/FlavorCategory
        [HttpPut("eat/random/{userId}/{flavorCategory}")]
        public IActionResult userFlavorCategory(int userId, string flavorCategory)
        {
            var repo = new CandyMarketRepository();
            var userFlavor = repo.EatRandomCandyByFlavor(userId, flavorCategory);
            return Ok(userFlavor);
        }

        // DELETE api/<CandyMarketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
