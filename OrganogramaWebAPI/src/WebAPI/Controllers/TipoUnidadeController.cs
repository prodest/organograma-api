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
    [Route("api/tipos-unidade")]
    public class TipoUnidadeController : Controller
    {
        ITipoUnidadeWorkService service;

        public TipoUnidadeController(ITipoUnidadeWorkService service)
        {
            this.service = service;
        }

        // GET: api/tipos-unidade
        [HttpGet]
        public IEnumerable<TipoUnidadeModelo> Get()
        {
            return service.Listar();
        }

        // GET api/tipos-unidade/{id}
        [HttpGet("{id}")]
        public TipoUnidadeModelo Get(int id)
        {
            return service.Pesquisar(id);
        }

        // POST api/tipos-unidade
        [Authorize]
        [HttpPost]
        public TipoUnidadeModelo Post([FromBody]TipoUnidadeModeloPost tipoUnidade)
        {
            return service.Inserir(tipoUnidade);
        }

        // PUT api/tipos-unidade/{id}
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]TipoUnidadeModeloPut tipoUnidade)
        {
            service.Alterar(id, tipoUnidade);
        }

        // DELETE api/tipos-unidade/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Excluir(id);
        }
    }
}
