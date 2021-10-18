using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCooking.Queries.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeCooking.Queries.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NutritionDataController : ControllerBase
    {
        // GET: api/NutritionData
        [HttpGet]
        public IEnumerable<Nutrition> Get()
        {
            return new Nutrition[]
            {
                new() {Item = "Pasta", Calories = 100},
                new() {Item = "Sausages", Calories = 200}
            };
        }

        // GET: api/NutritionData/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/NutritionData
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/NutritionData/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/NutritionData/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
