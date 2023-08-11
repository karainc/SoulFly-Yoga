using Microsoft.AspNetCore.Mvc;
using SoulFly.Models;
using SoulFly.Repositories;
using System;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoulFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_usersRepository.GetAllUsers());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var users = _usersRepository.GetUsersById(id);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("GetByEmail")]
        public IActionResult GetByEmail(string email)
        {
            var user = _usersRepository.GetByEmail(email);

            if (email == null || user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(Users users)
        {
            _usersRepository.Add(users);
            return CreatedAtAction(
                "GetByEmail",
                new { email = users.Email },
                users);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            _usersRepository.Update(users);
            return NoContent();
        }
    }
}