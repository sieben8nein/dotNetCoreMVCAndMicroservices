using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteService.Model;
using NoteService.Model.Entities;

namespace NoteService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _repo;
        public NoteController(INoteRepository repo)
        {
            _repo = repo;
        }
        // GET api/notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> Get()
        {
            return new ObjectResult(await _repo.GetAllNotes());
        }
        // GET api/notes/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> Get(long id)
        {
            var note = await _repo.GetNote(id);
            if (note == null)
                return new NotFoundResult();
            
            return new ObjectResult(note);
        }
        // POST api/notes
        [HttpPost]
        public async Task<ActionResult<Note>> Post([FromBody] Note note)
        {
            note.Id = await _repo.GetNextId();
            await _repo.Create(note);
            return new OkObjectResult(note);
        }
        // PUT api/notes/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Note>> Put(long id, [FromBody] Note note)
        {
            var noteFromDb = await _repo.GetNote(id);
            if (noteFromDb == null)
                return new NotFoundResult();
            note.Id = noteFromDb.Id;
            note.InternalId = noteFromDb.InternalId;
            await _repo.Update(note);
            return new OkObjectResult(note);
        }
        // DELETE api/notes/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await _repo.GetNote(id);
            if (post == null)
                return new NotFoundResult();
            await _repo.Delete(id);
            return new OkResult();
        }
    }
}
