using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Organograma.Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Microsoft.AspNetCore.Authorization;
using Organograma.WebAPI.Base;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/tipos-organizacao")]
    public class TipoOrganizacaoController : BaseController
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
        [HttpPost]
        [Authorize(Policy = "TipoOrganizacao.Inserir")]
        public TipoOrganizacaoModelo Post([FromBody]TipoOrganizacaoModeloPost tipoOrganizacao)
        {
            return service.Inserir(tipoOrganizacao);
        }

        // PUT api/tipos-organizacao/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = "TipoOrganizacao.Alterar")]
        public void Put(int id, [FromBody]TipoOrganizacaoModeloPut tipoOrganizacao)
        {
            service.Alterar(id, tipoOrganizacao);
        }

        // DELETE api/tipos-organizacao/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "TipoOrganizacao.Excluir")]
        public void Delete(int id)
        {
            service.Excluir(id);
        }
    }
}
