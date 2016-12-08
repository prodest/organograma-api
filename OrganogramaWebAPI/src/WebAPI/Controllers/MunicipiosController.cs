using Microsoft.AspNetCore.Mvc;
using Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Infraestrutura.Comum;
using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/municipios")]
    public class MunicipiosController : Controller
    {

        private IMunicipioWorkService service;

        public MunicipiosController(IMunicipioWorkService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Retorna os municípios todos os municípios, podendo ser filtrado por uma unidade da federação.
        /// </summary>
        /// <param name="uf">Sigla da unidade da federação a qual se deseja obter seus municípios.</param>
        /// <returns>Municípios, caso tenha sido informada uma unidade da federação, somente seus municípios.</returns>
        /// <response code="200">Retorna os municípios, caso tenha sido informada uma unidade da federação, somente seus municípios.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet]
        [ProducesResponseType(typeof(MunicipioModeloGet), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [Authorize]
        public IActionResult Listar([FromQuery] string uf)
        {
            try
            {
                return new ObjectResult(service.Listar(uf));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message); ;
            }
        }

        // GET api/municipios/{id}
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
        [Authorize]
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

        // PUT api/municipios/{id}
        [HttpPut("{id}")]
        [Authorize]
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

        // DELETE api/municipios/{id}
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
