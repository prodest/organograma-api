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
    [Route("api/unidades")]
    public class UnidadeController : BaseController
    {
        IUnidadeWorkService service;

        public UnidadeController(IUnidadeWorkService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Retorna as unidades da organização informada.
        /// </summary>
        /// <param name="guid">Identificador da organização a qual se deseja obter suas unidades.</param>
        /// <returns>Unidades da organização informada.</returns>
        /// <response code="200">Lista de unidades obtida com sucesso.</response>
        /// <response code="400">Falha na validação.</response>
        /// <response code="500">Erro inesperado.</response>
        [HttpGet("organizacao/{guid}")]
        [ProducesResponseType(typeof(List<UnidadeModeloGet>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarPorOrganizacao(string guid)
        {
            try
            {
                return new ObjectResult(service.PesquisarPorOrganizacao(guid));
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
        public IActionResult Get(string guid)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(guid));
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

        /// <summary>
        /// Insere uma unidade.
        /// </summary>
        /// <param name="unidade">Unidade que será inserida.</param>
        /// <returns>Unidade inserida.</returns>
        /// <response code="201">Unidade inserida com sucesso.</response>
        /// <response code="400">Falha na validação.</response>
        /// <response code="500">Erro inesperado.</response>
        [HttpPost]
        [Authorize(Policy = "Unidade.Inserir")]
        [ProducesResponseType(typeof(UnidadeModeloRetornoPost), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Post([FromBody]UnidadeModeloPost unidade)
        {
            try
            {
                UnidadeModeloRetornoPost unidadeModelo = service.Inserir(unidade);

                HttpRequest request = HttpContext.Request;
                return Created(request.Scheme + "://" + request.Host.Value + request.Path.Value + "/" + unidadeModelo.Id, unidadeModelo);
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
        /// Altera uma unidade.
        /// </summary>
        /// <param name="id">Identificador da unidade que será alterado.</param>
        /// <param name="unidade">Unidade que será alterada.</param>
        /// <response code="200">Unidade alterada com sucesso.</response>
        /// <response code="400">Falha na validação.</response>
        /// <response code="404">Unidade não encontrada.</response>
        /// <response code="500">Erro não esperado.</response>
        [HttpPatch("{id}")]
        [Authorize(Policy = "Unidade.Alterar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Patch(int id, [FromBody]UnidadeModeloPatch unidade)
        {
            try
            {
                service.Alterar(id, unidade);
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
        /// Exclui uma unidade.
        /// </summary>
        /// <param name="id">Identificador da unidade que será excluída.</param>
        /// <response code="200">Unidade excluída com sucesso.</response>
        /// <response code="400">Falha na validação.</response>
        /// <response code="404">Unidade não encontrada.</response>
        /// <response code="500">Erro inesperado.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Unidade.Excluir")]
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
