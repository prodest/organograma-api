using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Organograma.Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Microsoft.AspNetCore.Authorization;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/tipos-organizacao")]
    public class TipoOrganizacaoController : Controller
    {
        ITipoOrganizacaoWorkService service;

        public TipoOrganizacaoController(ITipoOrganizacaoWorkService service)
        {
            this.service = service;
        }

        // GET: api/tipos-organizacao
        [HttpGet]
        public IEnumerable<TipoOrganizacaoModelo> Get()
        {
            return service.Listar();
        }

        // GET api/tipos-organizacao/{id}
        [HttpGet("{id}")]
        public TipoOrganizacaoModelo Get(int id)
        {
            return service.Pesquisar(id);
        }

        // POST api/tipos-organizacao
        [Authorize]
        [HttpPost]
        public TipoOrganizacaoModelo Post([FromBody]TipoOrganizacaoModeloPost tipoOrganizacao)
        {
            return service.Inserir(tipoOrganizacao);
        }

        // PUT api/tipos-organizacao/{id}
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]TipoOrganizacaoModeloPut tipoOrganizacao)
        {
            service.Alterar(id, tipoOrganizacao);
        }

        // DELETE api/tipos-organizacao/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Excluir(id);
        }
    }
}
