using Microsoft.AspNetCore.Mvc;
using Apresentacao.Base;
using System.Collections.Generic;
using Organograma.Apresentacao.Modelos;
using Organograma.Infraestrutura.Comum;
using System;
using System.Net;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class MunicipiosController : Controller
    {

        private IMunicipioWorkService service;

        public MunicipiosController(IMunicipioWorkService service)
        {
            this.service = service;
        }

        // GET api/municipios
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

        // GET api/municipios/id
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

        // POST api/municipios
        [HttpPost]
        public IActionResult Post([FromBody]MunicipioModeloPost municipioPost)
        {
            try
            {
               return new ObjectResult(service.Inserir(municipioPost));
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

        // PUT api/municipios/id
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, [FromBody]MunicipioModeloPut municipioPut)
        {
            try
            {
                service.Alterar(id, municipioPut);
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

        // DELETE api/municipios/id
        [HttpDelete("{id}")]
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
