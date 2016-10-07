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

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Organograma.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class EsferaOrganizacaoController : Controller
    {
        IEsferaOrganizacaoWorkService service;

        public EsferaOrganizacaoController(IEsferaOrganizacaoWorkService service)
        {
            this.service = service;
        }

        // GET: api/esferaorganizacao
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
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET api/esferaorganizacao/5
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

        // POST api/esferaorganizacao
        [Authorize]
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

        // PUT api/esferaorganizacao/5
        [Authorize]
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

        // DELETE api/esferaorganizacao/5
        [Authorize]
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
