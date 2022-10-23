using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace restfull_api_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly StoreDbContext context;

        public PhonesController(StoreDbContext context)
        {
            this.context = context;
        }

        [HttpGet("collection")]          // GET: ~/api/phones/collection
        //[HttpGet("/phone-collection")] // GET: ~/phone-collection
        public IActionResult GetAll()
        {
            return Ok(context.Phones.ToList());
        }

        // GET: ~/api/phones/{id}
        [HttpGet]
        public IActionResult Get([FromRoute] int id) // get parameter from route URL
        {
            var phone = context.Phones.Find(id);

            if (phone == null) return NotFound();

            return Ok(phone);
        }

        // POST: ~/api/phones
        [HttpPost]
        public IActionResult Create([FromBody] Phone phone) // get parameter from body (JSON)
        {
            context.Phones.Add(phone);
            context.SaveChanges();

            return Ok();
        }

        // PUT: ~/api/phones
        [HttpPut]
        public IActionResult Edit([FromBody] Phone phone)
        {
            context.Phones.Update(phone);
            context.SaveChanges();

            return Ok();
        }

        // DELETE: ~/api/phones
        [HttpDelete]
        public IActionResult Delete([FromRoute] int id)
        {
            var phone = context.Phones.Find(id);

            if (phone == null) return NotFound();

            context.Phones.Remove(phone);
            context.SaveChanges();

            return Ok();
        }
    }
}
