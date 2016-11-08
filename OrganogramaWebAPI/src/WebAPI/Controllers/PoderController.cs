using Microsoft.AspNetCore.Mvc;
using Apresentacao.Base;
using System.Collections.Generic;
using Organograma.Apresentacao.Modelos;
using Organograma.Infraestrutura.Comum;
using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/poderes")]
    public class PoderController : Controller
    {

        private IPoderWorkService service;

        public PoderController(IPoderWorkService service)
        {
            this.service = service;
        }

        // GET api/poderes
        [HttpGet]
        public IActionResult Listar()
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
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message); ;
            }
        }

        // GET api/poderes/{id}
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

        // POST api/poderes/{id}
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]PoderModeloPost poderPost)
        {
            try
            {
               return new ObjectResult(service.Inserir(poderPost));
            }
            catch(OrganogramaRequisicaoInvalidaException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
            
        }

        // PUT api/poderes/{id}
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Alterar(int id, [FromBody]PoderModeloPut poderPut)
        {
            try
            {
                service.Alterar(id, poderPut);
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

        // DELETE api/poderes/{id}
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
