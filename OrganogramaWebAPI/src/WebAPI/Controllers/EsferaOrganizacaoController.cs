using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Organograma.Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Microsoft.AspNetCore.Authorization;
using Organograma.Infraestrutura.Comum;
using System.Net;
using Organograma.WebAPI.Base;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Organograma.WebAPI.Controllers
{
    [Route("api/esferas-organizacao")]
    public class EsferaOrganizacaoController : BaseController
    {
        IEsferaOrganizacaoWorkService service;

        public EsferaOrganizacaoController(IEsferaOrganizacaoWorkService service)
        {
            this.service = service;
        }

        // GET: api/esferas-organizaco
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return new ObjectResult(service.Listar());
            }
            catch (OrganogramaNaoEncontradoException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET api/esferas-organizacao/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(id));
            }
            catch (OrganogramaNaoEncontradoException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // POST api/esferas-organizacao
        [Authorize(Policy = "Esfera.Inserir")]
        [HttpPost]
        public IActionResult Post([FromBody]EsferaOrganizacaoModeloPost esferaOrganizacao)
        {
            try
            {
                return new ObjectResult(service.Inserir(esferaOrganizacao));
            }
            catch (OrganogramaRequisicaoInvalidaException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // PUT api/esferas-organizacao/5
        [Authorize(Policy = "Esfera.Alterar")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]EsferaOrganizacaoModelo esferaOrganizacao)
        {
            try
            {
                service.Alterar(id, esferaOrganizacao);

                return Ok();
            }
            catch (OrganogramaNaoEncontradoException e)
            {
                return NotFound(e.Message);
            }
            catch (OrganogramaRequisicaoInvalidaException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        // DELETE api/esferas-organizacao/5
        [Authorize(Policy = "Esfera.Excluir")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);

                return Ok();
            }
            catch (OrganogramaNaoEncontradoException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }
    }
}
