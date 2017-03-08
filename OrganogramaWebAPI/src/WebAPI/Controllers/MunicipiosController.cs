using Microsoft.AspNetCore.Mvc;
using Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Organograma.Infraestrutura.Comum;
using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Organograma.WebAPI.Base;
using System.Collections.Generic;
using Organograma.WebAPI.Config;
using Microsoft.AspNetCore.Http;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/municipios")]
    public class MunicipiosController : BaseController
    {
        private IMunicipioWorkService service;

        public MunicipiosController(IMunicipioWorkService service, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.service = service;
        }

        /// <summary>
        /// Retorna os municípios, podendo ser filtrado por uma unidade da federação.
        /// </summary>
        /// <param name="uf">Sigla da unidade da federação a qual se deseja obter seus municípios.</param>
        /// <returns>Municípios, caso tenha sido informada uma unidade da federação, somente seus municípios.</returns>
        /// <response code="200">Retorna os municípios, caso tenha sido informada uma unidade da federação, somente seus municípios.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<MunicipioModeloGet>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Listar([FromQuery] string uf)
        {
            try
            {
                return new ObjectResult(service.Listar(uf));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        /// <summary>
        /// Retorna as informações do município informado.
        /// </summary>
        /// <param name="guid">Identificador do município o qual se deseja obter suas informações.</param>
        /// <returns>Infomações do município informado.</returns>
        /// <response code="200">Retorna as informações do município informado.</response>
        /// <response code="400">Retorna a mensagem de falha na validação.</response>
        /// <response code="404">Município não foi encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{guid}")]
        [ProducesResponseType(typeof(OrganizacaoModeloGet), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Pesquisar(string guid)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(guid));
            }
            catch (OrganogramaRequisicaoInvalidaException e)
            {
                return BadRequest(e.Message);
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

        /// <summary>
        /// Insere um município.
        /// </summary>
        /// <param name="municipioPost">Município que será inserido.</param>
        /// <returns>Município inserido.</returns>
        /// <response code="201">Retorna o município inserido.</response>
        /// <response code="400">Retorna a descrição da invalidação.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPost]
        [Authorize(Policy = "Municipio.Inserir")]
        [ProducesResponseType(typeof(EsferaOrganizacaoModelo), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Post([FromBody]MunicipioModeloPost municipioPost)
        {
            try
            {
                MunicipioModeloGet municpio = service.Inserir(municipioPost);

                HttpRequest request = HttpContext.Request;
                return Created(request.Scheme + "://" + request.Host.Value + request.Path.Value + "/" + municpio.Guid, municpio);
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
        /// Altera um município.
        /// </summary>
        /// <param name="guid">Identificador do município que será alterado.</param>
        /// <param name="municipio">Município que será alterado.</param>
        /// <response code="200">Município alterado com sucesso.</response>
        /// <response code="400">Retorna a descrição da invalidação.</response>
        /// <response code="404">Município não encontrado.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpPut("{guid}")]
        [Authorize(Policy = "Municipio.Alterar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Alterar(string guid, [FromBody]MunicipioModeloPut municipio)
        {
            try
            {
                service.Alterar(guid, municipio);
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
        /// <param name="guid">Identificador da esfera de organizações que será excluída.</param>
        /// <response code="400">Retorna a descrição da invalidação.</response>
        /// <response code="200">Esfera de organizações excluída com sucesso.</response>
        /// <response code="404">Esfera de organizações não encontrada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpDelete("{guid}")]
        [Authorize(Policy = "Municipio.Excluir")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Excluir(string guid)
        {
            try
            {
                service.Excluir(guid);
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
