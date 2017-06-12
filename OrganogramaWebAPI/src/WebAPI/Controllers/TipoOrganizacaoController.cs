using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Route("api/tipos-organizacao")]
    public class TipoOrganizacaoController : BaseController
    {
        ITipoOrganizacaoWorkService service;

        public TipoOrganizacaoController(ITipoOrganizacaoWorkService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Retorna a lista de tipos de organização.
        /// </summary>
        /// <returns>Lista de tipos de organização.</returns>
        /// <response code="200">Retorna a lista de tipos de organização.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<TipoOrganizacaoModelo>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get()
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
        /// Retorna o tipo de organização conforme o identificador informado.
        /// </summary>
        /// <param name="id">Identificador do tipo de organização.</param>
        /// <returns>Tipo de organização conforme o identificador informado.</returns>
        /// <response code="200">Retorna o tipo de organização conforme o identificador informado.</response>
        /// <response code="404">Tipo de organização não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TipoOrganizacaoModelo), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int id)
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
        /// Insere um tipo de organização.
        /// </summary>
        /// <param name="tipoOrganizacao">Tipo de organização que será inserido.</param>
        /// <returns>Tipo de organização inserido.</returns>
        /// <response code="201">Tipo de organização inserido com sucesso.</response>
        /// <response code="400">Falha na validação.</response>
        /// <response code="500">Erro inesperado.</response>
        [HttpPost]
        [Authorize(Policy = "TipoOrganizacao.Inserir")]
        [ProducesResponseType(typeof(TipoOrganizacaoModelo), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Post([FromBody]TipoOrganizacaoModeloPost tipoOrganizacao)
        {
            try
            {
                TipoOrganizacaoModelo tipoOrganizacaoModelo = service.Inserir(tipoOrganizacao);

                HttpRequest request = HttpContext.Request;
                return Created(request.Scheme + "://" + request.Host.Value + request.Path.Value + "/" + tipoOrganizacaoModelo.Id, tipoOrganizacaoModelo);
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
        /// Altera um tipo de organização.
        /// </summary>
        /// <param name="id">Identificador do tipo de organização que será alterado.</param>
        /// <param name="tipoOrganizacao">Tipo de organização que será alterado.</param>
        /// <response code="200">Tipo de organização alterado com sucesso.</response>
        /// <response code="400">Falha na validação.</response>
        /// <response code="404">Tipo de organização não encontrado.</response>
        /// <response code="500">Erro não esperado.</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "TipoOrganizacao.Alterar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Put(int id, [FromBody]TipoOrganizacaoModeloPut tipoOrganizacao)
        {
            try
            {
                service.Alterar(id, tipoOrganizacao);
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
        /// Exclui um tipo de organização.
        /// </summary>
        /// <param name="id">Identificador do tipo de organização que será excluído.</param>
        /// <response code="200">Tipo de organização excluído com sucesso.</response>
        /// <response code="400">Falha na validação.</response>
        /// <response code="404">Tipo de organização não encontrado.</response>
        /// <response code="500">Erro inesperado.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "TipoOrganizacao.Excluir")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Delete(int id)
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
