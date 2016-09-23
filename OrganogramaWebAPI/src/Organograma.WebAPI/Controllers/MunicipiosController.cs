using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Apresentacao.Municipio.Base;
using Organograma.Apresentacao.Modelos;
using Microsoft.AspNetCore.Authorization;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class MunicipiosController : Controller
    {
        
        private IMunicipioWorkService service;

        public MunicipiosController (IMunicipioWorkService service)
        {
            this.service = service;
        }

        // GET api/municipios
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(service.ConsultarMunicipios());
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
