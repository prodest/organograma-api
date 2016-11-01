using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Organograma.Infraestrutura.Comum;
using System;
using System.Net;
using Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrganizacaoController : Controller
    {

        private IOrganizacaoWorkService service;

        public OrganizacaoController(IOrganizacaoWorkService service)
        {
            this.service = service;
        }
        
        // GET: api/organizacao
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return new ObjectResult(service.Listar());
                //return new ObjectResult("Get");
            }

            catch (OrganogramaNaoEncontradoException e)
            {
                return NotFound(e.Message);
            }

            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message); ;
            }
        }

        // GET api/organizacao/5
        [HttpGet("{id}")]
        public IActionResult Pesquisar(int id)
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

        // POST api/organizacao
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]OrganizacaoModeloPost organizacaoPost)
        {

            try
            {
                return new ObjectResult(service.Inserir(organizacaoPost));
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

        // Patch api/organizacao/{id}
        [HttpPatch("{id}")]
        [Authorize]
        public IActionResult AlterarOrganizacao(int id, [FromBody]OrganizacaoModeloPatch organizacao)
        {
            try
            {
                service.Alterar(id, organizacao);
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Excluir(int id)
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
            catch (OrganogramaRequisicaoInvalidaException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
