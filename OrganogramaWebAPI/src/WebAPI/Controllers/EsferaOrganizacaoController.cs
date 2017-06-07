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

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Organograma.WebAPI.Controllers
{
    [Route("api/esferas-organizacao")]
    public class EsferaOrganizacaoController : BaseController
    {
        IEsferaOrganizacaoWorkService service;

        public EsferaOrganizacaoController(IEsferaOrganizacaoWorkService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Retorna a lista de esferas de organizações.
        /// </summary>
        /// <returns>Lista de esferas de organizações.</returns>
        /// <response code="200">Retorna a lista de esferas de organizações.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<EsferaOrganizacaoModelo>), 200)]
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
        /// Retorna a esfera de organizações conforme o identificador informado.
        /// </summary>
        /// <param name="id">Identificador da esfera de organizações.</param>
        /// <returns>Esfera de organizações conforme o identificador informado.</returns>
        /// <response code="200">Retorna a esfera de organizações conforme o identificador informado.</response>
        /// <response code="404">Esfera de organizações não encontrada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EsferaOrganizacaoModelo), 200)]
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
        /// Insere uma esfera de organizações.
        /// </summary>
        /// <param name="esferaOrganizacao">Esfera de organizações que será inserida.</param>
        /// <returns>Esfera de organizações inserida.</returns>
        /// <response code="201">Retorna a esfera de organizações inserida.</response>
        /// <response code="400">Retorna a descrição da invalidação.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPost]
        [Authorize(Policy = "Esfera.Inserir")]
        [ProducesResponseType(typeof(EsferaOrganizacaoModelo), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Post([FromBody]EsferaOrganizacaoModeloPost esferaOrganizacao)
        {
            try
            {
                EsferaOrganizacaoModelo esfera = service.Inserir(esferaOrganizacao);

                HttpRequest request = HttpContext.Request;
                return Created(request.Scheme + "://" + request.Host.Value + request.Path.Value + "/" + esfera.Id, esfera);
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
        /// Altera uma esfera de organizações.
        /// </summary>
        /// <param name="id">Identificador da esfera de organizações que será alterada.</param>
        /// <param name="esferaOrganizacao">Esfera de organizações que será alterada.</param>
        /// <response code="200">Esfera de organizações alterada com sucesso.</response>
        /// <response code="400">Retorna a descrição da invalidação.</response>
        /// <response code="404">Esfera de organizações não encontrada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "Esfera.Alterar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Put(int id, [FromBody]EsferaOrganizacaoModelo esferaOrganizacao)
        {
            try
            {
                service.Alterar(id, esferaOrganizacao);

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
        /// Exclui uma esfera de organizações.
        /// </summary>
        /// <param name="id">Identificador da esfera de organizações que será excluída.</param>
        /// <response code="200">Esfera de organizações excluída com sucesso.</response>
        /// <response code="404">Esfera de organizações não encontrada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Esfera.Excluir")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Delete(int id)
        {
            try
            {
                service.Excluir(id);

                return Ok();
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
