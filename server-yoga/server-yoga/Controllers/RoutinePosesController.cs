using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using server_yoga.Repositories;
using server_yoga.Models;
using server_yogaRepositories;

namespace server_yoga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutinePosesController : ControllerBase
    {
        private readonly IRoutinePosesRepository _routinePosesRepository;
        public RoutinePosesController(IRoutinePosesRepository routinePosesRepository)
        {
            _routinePosesRepository = routinePosesRepository;
        }
        ////get all routinePoses
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok(_routinePosesRepository.GetAllPoses());
        //}

        //get a routines pose by id
        [HttpGet("{id}")]
        public IActionResult GetPosesForRoutine(int id)
        {
            var routinePoses = _routinePosesRepository.GetAllRoutinesPoses(id);
            if (routinePoses == null)
            {
                return NotFound(); //if there are no poses
            }
            return Ok(routinePoses); //if there are poses
        }


        //add a pose to a routine
        [HttpPost]
        public IActionResult AddRoutinePose(RoutinePoses routinePoses)
        {
            _routinePosesRepository.AddPosesToRoutine(routinePoses);
            return CreatedAtAction("Get", new { id = routinePoses.Id }, routinePoses);
        }
        //delete a pose
        [HttpDelete("{id}")]
        public IActionResult DeleteRoutinePose(int routineId, int poseId)
        { 
            _routinePosesRepository.DeletePoseFromRoutine(routineId, poseId);
            return NoContent();
        }



    }
}