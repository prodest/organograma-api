using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Organograma.Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Organograma.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TipoUnidadeController : Controller
    {
        ITipoUnidadeWorkService service;

        public TipoUnidadeController(ITipoUnidadeWorkService service)
        {
            this.service = service;
        }

        // GET: api/tipounidade
        [HttpGet]
        public IEnumerable<TipoUnidadeModelo> Get()
        {
            return service.Listar();
        }

        // GET api/tipounidade/5
        [HttpGet("{id}")]
        public TipoUnidadeModelo Get(int id)
        {
            return service.Pesquisar(id);
        }

        // POST api/tipounidade
        [Authorize]
        [HttpPost]
        public TipoUnidadeModelo Post([FromBody]TipoUnidadeModeloPost tipoUnidade)
        {
            return service.Incluir(tipoUnidade);
        }

        // PUT api/tipounidade/5
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]TipoUnidadeModeloPut tipoUnidade)
        {
            service.Alterar(id, tipoUnidade);
        }

        // DELETE api/tipounidade/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Excluir(id);
        }
    }
}
