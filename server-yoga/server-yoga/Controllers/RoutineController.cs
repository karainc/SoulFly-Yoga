using Microsoft.AspNetCore.Mvc;
using server_yoga.Models;
using server_yoga.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.Extensions.Hosting;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server_yoga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutineController : ControllerBase
    {
        private readonly IRoutineRepository _routineRepo;

        public RoutineController(IRoutineRepository routineRepository)
        {
            _routineRepo = routineRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_routineRepo.GetAllRoutines());
        }
        // GET: api/<RoutineController>
        [HttpGet("GetUsersRoutines/{id}")]
        public IActionResult Get(int id)
        {
            List<Routine> routines = _routineRepo.GetRoutinesByUserId(id);
            if (routines == null)
            {
                return NotFound();
            }
            return Ok(routines);
        }

        // GET api/<RoutineController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Routine routine = _routineRepo.GetRoutineById(id);
            if (routine == null)
            {
                return NotFound();
            }
            return Ok(routine);
        }
        [HttpPost]
        public IActionResult Post (Routine routine)
        {
            _routineRepo.AddRoutine(routine);
            return CreatedAtAction("Get", new { id = routine.Id }, routine);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, Routine routine)
        {
            if (id != routine.Id)
            {
                return BadRequest();
            }
            _routineRepo.UpdateRoutine(routine);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _routineRepo.DeleteRoutine(id);
            return NoContent();
        }
    }
}
