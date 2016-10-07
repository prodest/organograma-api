using Microsoft.AspNetCore.Mvc;
using Apresentacao.Base;
using System.Collections.Generic;
using Organograma.Apresentacao.Modelos;

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
        public IEnumerable<MunicipioModeloGet> Listar()
        {
            return service.Listar();
        }

        // GET api/municipios/id
        [HttpGet("{int}")]
        public MunicipioModeloGet Pesquisar(int id)
        {
            return service.Pesquisar(id);
        }

        // POST api/municipios
        [HttpPost]
        public MunicipioModeloGet Post([FromBody]MunicipioModeloPost municipioPost)
        {
            service.Inserir(municipioPost);
            return new MunicipioModeloGet();
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
