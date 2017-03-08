using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Organograma.Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Microsoft.AspNetCore.Authorization;
using Organograma.WebAPI.Base;
using System.Net;
using Organograma.WebAPI.Config;
using Organograma.Infraestrutura.Comum;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Organograma.WebAPI.Controllers
{
    [Route("api/tipos-unidade")]
    public class TipoUnidadeController : BaseController
    {
        ITipoUnidadeWorkService service;

        public TipoUnidadeController(ITipoUnidadeWorkService service, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.service = service;
        }

        /// <summary>
        /// Retorna a lista de tipos de unidade.
        /// </summary>
        /// <returns>Lista de tipos de unidade.</returns>
        /// <response code="200">Lista de tipos de unidade obtida com sucesso.</response>
        /// <response code="500">Erro inesperado.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<TipoUnidadeModelo>), 200)]
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
        /// Retorna o tipo de unidade conforme o identificador informado.
        /// </summary>
        /// <param name="id">Identificador do tipo de unidade.</param>
        /// <returns>Tipo de unidade conforme o identificador informado.</returns>
        /// <response code="200">Tipo de unidade obtida com sucesso.</response>
        /// <response code="404">Tipo de unidade não encontrado.</response>
        /// <response code="500">Erro inesperado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TipoUnidadeModelo), 200)]
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
        /// Insere um tipo de unidade.
        /// </summary>
        /// <param name="tipoUnidade">Tipo de unidade que será inserido.</param>
        /// <returns>Tipo de unidade inserido.</returns>
        /// <response code="201">Tipo de unidade inserido com sucesso.</response>
        /// <response code="400">Falha na validação.</response>
        /// <response code="500">Erro inesperado.</response>
        [HttpPost]
        [Authorize(Policy = "TipoUnidade.Inserir")]
        [ProducesResponseType(typeof(TipoUnidadeModelo), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Post([FromBody]TipoUnidadeModeloPost tipoUnidade)
        {
            try
            {
                TipoUnidadeModelo tipoUnidadeModelo = service.Inserir(tipoUnidade);

                HttpRequest request = HttpContext.Request;
                return Created(request.Scheme + "://" + request.Host.Value + request.Path.Value + "/" + tipoUnidadeModelo.Id, tipoUnidadeModelo);
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
        /// Altera um tipo de unidade.
        /// </summary>
        /// <param name="id">Identificador do tipo de unidade que será alterado.</param>
        /// <param name="tipoUnidade">Tipo de unidade que será alterado.</param>
        /// <response code="200">Tipo de unidade alterado com sucesso.</response>
        /// <response code="400">Falha na validação.</response>
        /// <response code="404">Tipo de unidade não encontrado.</response>
        /// <response code="500">Erro não esperado.</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "TipoUnidade.Alterar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Put(int id, [FromBody]TipoUnidadeModeloPut tipoUnidade)
        {
            try
            {
                service.Alterar(id, tipoUnidade);
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
        /// Exclui um tipo de unidade.
        /// </summary>
        /// <param name="id">Identificador do tipo de unidade que será excluído.</param>
        /// <response code="200">Tipo de unidade excluído com sucesso.</response>
        /// <response code="400">Falha na validação.</response>
        /// <response code="404">Tipo de unidade não encontrado.</response>
        /// <response code="500">Erro inesperado.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "TipoUnidade.Excluir")]
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
