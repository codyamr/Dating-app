using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public DataContext _Context { get; }

        // GET api/values
        public ValuesController(DataContext context)
        {
            _Context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetValues()
        {
           var vlaues= await _Context.Values.ToListAsync();
            return Ok(vlaues);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetValue(int id)
        {
            var vlaue  = await _Context.Values.FirstOrDefaultAsync(i=> i.Id == id);
            return Ok(vlaue);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
