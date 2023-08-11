using Microsoft.AspNetCore.Mvc;
using SoulFly.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SoulFly.Repositories;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoulFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosesController : ControllerBase
    {
        private readonly IPosesRepository _posesRepo;

        public PosesController(IPosesRepository posesRepository)
        {
            _posesRepo = posesRepository;
        }
        // GET: api/<PosesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_posesRepo.GetAllPoses());
        }

        // GET api/<PosesController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Poses pose = _posesRepo.GetPosesById(id);
            if (pose == null)
            {
                return NotFound();
            }
            return Ok(pose);
        }

    } }

