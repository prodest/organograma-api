using Microsoft.AspNetCore.Mvc;
using Apresentacao.Municipio.Base;


namespace Organograma.WebAPI.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult Get(int id)
        {
            return new ObjectResult(service.ConsultarMunicipioPorId(id));
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
