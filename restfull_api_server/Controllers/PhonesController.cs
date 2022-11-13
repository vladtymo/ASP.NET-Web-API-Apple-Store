using Core.DTOs;
using Core.Exceptions;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;

namespace restfull_api_server.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // user authorization is nedded
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly IPhoneService phoneService;

        public PhonesController(IPhoneService phoneService)
        {
            this.phoneService = phoneService;
        }

        [HttpGet("collection")]          // GET: ~/api/phones/collection
        //[HttpGet("/phone-collection")] // GET: ~/phone-collection
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(phoneService.GetAll());
        }


        // GET: ~/api/phones/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get([FromRoute] int id) // get parameter from route URL
        {
            return Ok(phoneService.Get(id));
        }

        // POST: ~/api/phones
        [HttpPost]
        public IActionResult Create([FromBody] PhoneDTO phone) // get parameter from body (JSON)
        {
            if (!ModelState.IsValid) return BadRequest();

            phoneService.Create(phone);

            return Ok();
        }

        // PUT: ~/api/phones
        [HttpPut]
        public IActionResult Edit([FromBody] PhoneDTO phone)
        {
            if (!ModelState.IsValid) return BadRequest();

            phoneService.Edit(phone);

            return Ok();
        }

        // DELETE: ~/api/phones
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            phoneService.Delete(id);
            return Ok();
        }
    }
}
