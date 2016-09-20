using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class MunicipiosController : Controller
    {
        // GET api/municipios
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Município 1", "Município 2" };
        }

        // GET api/municipios/id
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return id.ToString();
        }

        // POST api/municipios
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/municipios/id
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/municipios/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
