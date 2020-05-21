﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyMarket.DataAccess;
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

        // GET api/<CandyMarketController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CandyMarketController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CandyMarketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CandyMarketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}