using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using server_yoga.Models;
using server_yoga.Repositories;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server_yoga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepo = commentRepository;
        }

        // GET: api/<CommentController>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            List<Comment> comment = _commentRepo.GetAllByRoutineId(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        // GET api/<CommentController>/5
        [HttpGet("commentById/{id}")]
        public IActionResult GetById(int id)
        {
            Comment comment = _commentRepo.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        // POST api/<CommentController>
        [HttpPost]
        public ActionResult Post(Comment comment)
        {
            _commentRepo.AddComment(comment);
            return CreatedAtAction("Get", new { id = comment.Id }, comment);
        }

        // PUT api/localhost:7451/api/comment/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _commentRepo.DeleteComment(id);
            return NoContent();
        }

        //UPDATE api/<CommentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }
            _commentRepo.UpdateComment(comment);
            return NoContent();
        }
    }
}
