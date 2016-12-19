using Microsoft.AspNetCore.Mvc;
using Apresentacao.Base;
using System.Collections.Generic;
using Organograma.Apresentacao.Modelos;
using Organograma.Infraestrutura.Comum;
using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Organograma.WebAPI.Base;
using Organograma.WebAPI.Config;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/poderes")]
    public class PoderController : BaseController
    {
        private IPoderWorkService service;

        public PoderController(IPoderWorkService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Retorna a lista de poderes de organizações.
        /// </summary>
        /// <returns>Lista de poderes de organizações.</returns>
        /// <response code="200">Retorna a lista de poderes de organizações.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<PoderModeloGet>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Listar()
        {
            try
            {
                return new ObjectResult(service.Listar());
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        /// <summary>
        /// Retorna o poder de organizações conforme o identificador informado.
        /// </summary>
        /// <param name="id">Identificador do poder de organizações.</param>
        /// <returns>Poder de organizações conforme o identificador informado.</returns>
        /// <response code="200">Retorna o poder de organizações conforme o identificador informado.</response>
        /// <response code="404">Poder de organizações não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PoderModeloGet), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Pesquisar(int id)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(id));
            }
            catch (OrganogramaNaoEncontradoException e)
            {
                return NotFound(MensagemErro.ObterMensagem(e));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        /// <summary>
        /// Insere um poder de organizações.
        /// </summary>
        /// <param name="poder">Poder de organizações que será inserido.</param>
        /// <returns>Poder de organizações inserido.</returns>
        /// <response code="201">Retorna o poder de organizações inserido.</response>
        /// <response code="400">Retorna a descrição da invalidação.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPost]
        [Authorize(Policy = "Poder.Inserir")]
        [ProducesResponseType(typeof(PoderModeloGet), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Post([FromBody]PoderModeloPost poder)
        {
            try
            {
               return new ObjectResult(service.Inserir(poder));
            }
            catch(OrganogramaRequisicaoInvalidaException e)
            {
                return BadRequest(MensagemErro.ObterMensagem(e));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
            
        }

        /// <summary>
        /// Altera um poder de organizações.
        /// </summary>
        /// <param name="id">Identificador do poder de organizações que será alterado.</param>
        /// <param name="poder">Poder de organizações que será alterado.</param>
        /// <response code="200">Poder de organizações alterado com sucesso.</response>
        /// <response code="400">Retorna a descrição da invalidação.</response>
        /// <response code="404">Poder de organizações não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "Poder.Alterar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Alterar(int id, [FromBody]PoderModeloPut poder)
        {
            try
            {
                service.Alterar(id, poder);
                return Ok();
            }
            catch (OrganogramaNaoEncontradoException e)
            {
                return NotFound(MensagemErro.ObterMensagem(e));
            }
            catch (OrganogramaRequisicaoInvalidaException e)
            {
                return BadRequest(MensagemErro.ObterMensagem(e));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        /// <summary>
        /// Exclui um poder de organizações.
        /// </summary>
        /// <param name="id">Identificador do poder de organizações que será excluído.</param>
        /// <response code="200">Poder de organizações excluído com sucesso.</response>
        /// <response code="400">Retorna a descrição da invalidação.</response>
        /// <response code="404">Poder de organizações não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Poder.Excluir")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Excluir(int id)
        {
            try
            {
                service.Excluir(id);
                return Ok();
            }
            catch (OrganogramaRequisicaoInvalidaException e)
            {
                return BadRequest(MensagemErro.ObterMensagem(e));
            }
            catch (OrganogramaNaoEncontradoException e)
            {
                return NotFound(MensagemErro.ObterMensagem(e));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }
    }
}
