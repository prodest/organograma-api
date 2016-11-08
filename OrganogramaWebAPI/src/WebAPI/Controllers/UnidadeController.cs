using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organograma.Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Infraestrutura.Comum;
using System;
using System.Collections.Generic;
using System.Net;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/unidades")]
    public class UnidadeController : Controller
    {
        IUnidadeWorkService service;

        public UnidadeController(IUnidadeWorkService service)
        {
            this.service = service;
        }

        // GET: api/unidades
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

        // GET api/unidades/{id}
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

        // POST api/unidades
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]UnidadeModeloPost unidade)
        {
            try
            {
                return new ObjectResult(service.Inserir(unidade));
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

        // PATCH api/unidades/{id}
        [Authorize]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]UnidadeModeloPatch unidade)
        {
            try
            {
                service.Alterar(id, unidade);

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

        // DELETE api/unidades/{id}
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

        [HttpPost("{id}/email")]
        public IActionResult InserirEmail(int id, [FromBody]List<EmailModelo> emails)
        {
            try
            {
                //service.ExcluirEmail(id, emails);

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
        [HttpDelete("{id}/email")]
        public IActionResult DeleteEmail(int id, [FromBody]List<EmailModelo> emails)
        {
            try
            {
                service.ExcluirEmail(id, emails);

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
