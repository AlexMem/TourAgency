using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TourAgency.Contexts;

namespace TourAgency.Controllers {
    [Route("api/[controller]")]
    public class ValuesController : Controller {    
        private readonly TourAgencyDbContext dbContext;

        public ValuesController(TourAgencyDbContext dbContext) {
            this.dbContext = dbContext;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<object> Get() {
            using (dbContext) {
                return dbContext.roles.ToList();
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value) {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}