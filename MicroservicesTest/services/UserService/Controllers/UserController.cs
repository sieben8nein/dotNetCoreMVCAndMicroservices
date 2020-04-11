using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Kafka;
using UserService.Model;
using UserService.Model.Entities;

namespace UserService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IUserProducer _producer;
        public UserController(IUserRepository repo, IUserProducer producer)
        {
            _repo = repo;
            _producer = producer;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            _producer.produce("get user");
            return new ObjectResult(await _repo.GetAllUsers());
            
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<User>> Get(long id)
        {
            var user = await _repo.GetUser(id);
            if (user == null)
                return new NotFoundResult();
            

            return new ObjectResult(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            user.Id = await _repo.GetNextId();
            await _repo.Create(user);
            return new OkObjectResult(user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(long id, [FromBody] User user)
        {
            var userFromDb = await _repo.GetUser(id);
            if (userFromDb == null)
                return new NotFoundResult();
            user.Id = userFromDb.Id;
            user.InternalId = userFromDb.InternalId;
            await _repo.Update(user);
            return new OkObjectResult(user);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(long id)
        {
            var post = await _repo.GetUser(id);
            if (post == null)
                return new NotFoundResult();
            await _repo.Delete(id);
            return new OkResult();
        }
    }
}
