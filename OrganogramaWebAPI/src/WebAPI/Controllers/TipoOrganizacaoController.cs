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
    public class TipoOrganizacaoController : Controller
    {
        ITipoOrganizacaoWorkService service;

        public TipoOrganizacaoController(ITipoOrganizacaoWorkService service)
        {
            this.service = service;
        }

        // GET: api/tipoorganizacao
        [HttpGet]
        public IEnumerable<TipoOrganizacaoModelo> Get()
        {
            return service.Listar();
        }

        // GET api/tipoorganizacao/5
        [HttpGet("{id}")]
        public TipoOrganizacaoModelo Get(int id)
        {
            return service.Pesquisar(id);
        }

        // POST api/tipoorganizacao
        [Authorize]
        [HttpPost]
        public TipoOrganizacaoModelo Post([FromBody]TipoOrganizacaoModeloPost tipoOrganizacao)
        {
            return service.Inserir(tipoOrganizacao);
        }

        // PUT api/tipoorganizacao/5
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]TipoOrganizacaoModeloPut tipoOrganizacao)
        {
            service.Alterar(id, tipoOrganizacao);
        }

        // DELETE api/tipoorganizacao/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Excluir(id);
        }
    }
}
