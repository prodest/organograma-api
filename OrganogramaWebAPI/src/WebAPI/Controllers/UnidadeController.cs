using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organograma.Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Infraestrutura.Comum;
using Organograma.WebAPI.Base;
using Organograma.WebAPI.Config;
using System;
using System.Collections.Generic;
using System.Net;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/unidades")]
    public class UnidadeController : BaseController
    {
        IUnidadeWorkService service;

        public UnidadeController(IUnidadeWorkService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Retorna as informações da unidade informada.
        /// </summary>
        /// <param name="guid">Identificador da organização a qual se deseja obter suas informações.</param>
        /// <returns>Informações da unidade informada.</returns>
        /// <response code="200">Retorna as informações da unidadeinformada.</response>
        /// <response code="400">Retorna a descrição do problema encontrado.</response>
        /// <response code="404">Unidade não encontrada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{guid}")]
        [ProducesResponseType(typeof(UnidadeModeloGet), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        [Authorize]
        public IActionResult Get(string guid)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(guid)); 
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

        /// <summary>
        /// Retorna as unidades da organização informada.
        /// </summary>
        /// <param name="guid">Identificador da organização a qual se deseja obter suas unidades.</param>
        /// <returns>Unidades da organização informada.</returns>
        /// <response code="200">Retorna as unidades da organização informada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("organizacao/{guid}")]
        [ProducesResponseType(typeof(List<UnidadeModeloGet>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [Authorize]
        public IActionResult PesquisarPorOrganizacao(string guid)
        {
            try
            {
                return new ObjectResult(service.PesquisarPorOrganizacao(guid));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        // POST api/unidades
        [HttpPost]
        [Authorize(Policy = "Unidade.Inserir")]
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
        [HttpPatch("{id}")]
        [Authorize(Policy = "Unidade.Alterar")]
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
        [HttpDelete("{id}")]
        [Authorize(Policy = "Unidade.Excluir")]
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
        [Authorize(Policy = "Unidade.Alterar")]
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
        [Authorize(Policy = "Unidade.Alterar")]
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
