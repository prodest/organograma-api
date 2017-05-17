using Apresentacao.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organograma.WebAPI.Base;
using Organograma.WebAPI.Config;
using Organograma.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/integracao")]
    public class IntegracaoController : BaseController
    {
        IOrganizacaoWorkService _service;
        IClientAccessToken _clientAccessToken;

        public IntegracaoController(IOrganizacaoWorkService organizacaoService, IHttpContextAccessor httpContextAccessor, IClientAccessToken clientAccessToken) : base(organizacaoService, httpContextAccessor, clientAccessToken)
        {
            _service = organizacaoService;
            _clientAccessToken = clientAccessToken;
        }

        /// <summary>
        /// Realiza a integração com o  SIARHES.
        /// </summary>
        /// <returns>Lista de esferas de organizações.</returns>
        /// <response code="200">Retorna a lista de esferas de organizações.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [AllowAnonymous]
        [HttpGet("siarhes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var a = await DownloadJsonData<List<OrganizacaoSiarhes>>("https://api.es.gov.br/siarhes/v1/subempresas", _clientAccessToken.AccessToken);

                return new ObjectResult(a);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        /// <summary>
        /// Realiza a integração com o  SIARHES.
        /// </summary>
        /// <returns>Lista de esferas de organizações.</returns>
        /// <response code="200">Retorna a lista de esferas de organizações.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [AllowAnonymous]
        [HttpGet("siarhes2")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Get2()
        {
            try
            {
                var a = await DownloadJsonData<List<UnidadeSiarhes>>("https://api.es.gov.br/siarhes/v1/organograma", _clientAccessToken.AccessToken);

                return new ObjectResult(a);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }
        /// <summary>
        /// Realiza a integração com o  SIARHES.
        /// </summary>
        /// <returns>Lista de esferas de organizações.</returns>
        /// <response code="200">Retorna a lista de esferas de organizações.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [AllowAnonymous]
        [HttpGet("siarhes3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Get3()
        {
            try
            {
                var a = await DownloadJsonData<object>("https://api.es.gov.br/siarhes/v1/subempresas", _clientAccessToken.AccessToken);

                return new ObjectResult(a);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        /// <summary>
        /// Realiza a integração com o  SIARHES.
        /// </summary>
        /// <returns>Lista de esferas de organizações.</returns>
        /// <response code="200">Retorna a lista de esferas de organizações.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [AllowAnonymous]
        [HttpGet("siarhes4")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Get4()
        {
            try
            {
                var a = await DownloadJsonData<object>("https://api.es.gov.br/siarhes/v1/organograma", _clientAccessToken.AccessToken);

                return new ObjectResult(a);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }
    }
}
