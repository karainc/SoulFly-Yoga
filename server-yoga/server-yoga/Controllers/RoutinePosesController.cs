using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server_yoga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutinePosesController : ControllerBase
    {
        // GET: api/<RoutinePosesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RoutinePosesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoutinePosesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RoutinePosesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RoutinePosesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
