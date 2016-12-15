using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Organograma.Infraestrutura.Comum;
using System;
using System.Net;
using Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Organograma.WebAPI.Config;
using Organograma.WebAPI.Base;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/organizacoes")]
    public class OrganizacaoController : BaseController
    {

        private IOrganizacaoWorkService service;

        public OrganizacaoController(IOrganizacaoWorkService service)
        {
            this.service = service;
        }

        #region GET

        // GET: api/organizacoes
        [HttpGet]
        public IActionResult Listar([FromQuery] string esfera, [FromQuery] string poder, [FromQuery] string uf, [FromQuery] int cod_ibge_municipio)
        {
            try
            {
                return new ObjectResult(service.Listar(esfera, poder, uf, cod_ibge_municipio));
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
        /// Retorna as informações da organização informada.
        /// </summary>
        /// <param name="guid">Identificador da organização a qual se deseja obter suas informações.</param>
        /// <returns>Infomações da organização informada.</returns>
        /// <response code="200">Retorna as informações da organização informada.</response>
        /// <response code="404">Organização não foi encontrada.</response>
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
        /// Retorna as informações da organização informada.
        /// </summary>
        /// <param name="sigla">Sigla da organização a qual se deseja obter suas informações.</param>
        /// <returns>Infomações da organização informada.</returns>
        /// <response code="200">Retorna as informações da organização informada.</response>
        /// <response code="404">Organização não foi encontrada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("sigla/{sigla}")]
        [ProducesResponseType(typeof(OrganizacaoModeloGet), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarPorSigla(string sigla)
        {
            try
            {
                return new ObjectResult(service.PesquisarPorSigla(sigla));
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
        /// Retorna a organização patriarca da organização informada.
        /// </summary>
        /// <param name="guid">Identificador da organização a qual se deseja obter a sua patriarca.</param>
        /// <returns>Organização patriarca da organização informada.</returns>
        /// <response code="200">Retorna a organização patriarca da organização informada.</response>
        /// <response code="404">Organização não foi encontrada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{guid}/patriarca")]
        [ProducesResponseType(typeof(OrganizacaoModeloGet), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarPatriarca(string guid)
        {
            try
            {
                return new ObjectResult(service.PesquisarPatriarca(guid));
            }
            catch (OrganogramaNaoEncontradoException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        /// <summary>
        /// Retorna a lista de organizações filhas e subfilhas da organização informada.
        /// </summary>
        /// <param name="guid">Identificador da organização a qual se deseja obter a lista de organizações filhas e subfilhas.</param>
        /// <returns>Lista de organizações filhas e subfilhas da organização informada.</returns>
        /// <response code="200">Retorna a lista de organizações filhas e subfilhas da organização informada.</response>
        /// <response code="500">Retorna a descrição do erro.</response>
        [HttpGet("{guid}/filhas")]
        [ProducesResponseType(typeof(List<OrganizacaoModeloGet>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarFilhas(string guid)
        {
            try
            {
                return new ObjectResult(service.PesquisarFilhas(guid));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }
        }

        #endregion

        #region POST
        // POST api/organizacoes
        [HttpPost]
        [Authorize(Policy = "Organizacao.Inserir")]
        public IActionResult Post([FromBody]OrganizacaoModeloPost organizacaoPost)
        {

            try
            {
                return new ObjectResult(service.Inserir(organizacaoPost));
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

        //Post api/organizacoes/{id}/site
        [HttpPost("{idOrganizacao}/site")]
        [Authorize(Policy = "Organizacao.Alterar")]
        public IActionResult PostSite(int idOrganizacao, [FromBody]SiteModelo sitePost)
        {

            try
            {
                return new ObjectResult(service.InserirSite(idOrganizacao, sitePost));
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
        #endregion

        #region PATCH
        // Patch api/organizacoes/{id}
        [HttpPatch("{id}")]
        [Authorize(Policy = "Organizacao.Alterar")]
        public IActionResult AlterarOrganizacao(int id, [FromBody]OrganizacaoModeloPatch organizacao)
        {
            try
            {
                service.Alterar(id, organizacao);
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

        #endregion

        #region DELETE
        // DELETE api/organizacoes/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "Organizacao.Excluir")]
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
        #endregion
    }
}
