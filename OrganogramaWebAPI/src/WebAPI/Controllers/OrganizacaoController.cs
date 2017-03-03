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

        /// <summary>
        /// Retorna a lista de organizações. Essa lista pode ser filtrada por esfera, poder, uf e/ou município
        /// </summary>
        /// <returns>Lista de organizações</returns>
        /// <param name="esfera">Descrição da esfera</param>
        /// <param name="poder">Descrição do poder</param>
        /// <param name="uf">UF</param>
        /// <param name="cod_ibge_municipio">Código IBGE do município</param>
        /// <response code="200">Retorna a lista de organizações</response>
        /// <response code="500">Erro inesperado.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<OrganizacaoModeloGet>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Listar([FromQuery] string esfera, [FromQuery] string poder, [FromQuery] string uf, [FromQuery] int cod_ibge_municipio)
        {
            try
            {
                return new ObjectResult(service.Listar(esfera, poder, uf, cod_ibge_municipio));
            }

            catch (OrganogramaNaoEncontradoException e)
            {
                return NotFound(MensagemErro.ObterMensagem(e));
            }
            
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, (MensagemErro.ObterMensagem(e)));
            }
        }

        /// <summary>
        /// Retorna as informações da organização informada.
        /// </summary>
        /// <param name="guid">Identificador da organização a qual se deseja obter suas informações.</param>
        /// <returns>Infomações da organização informada.</returns>
        /// <response code="200">Retorna as informações da organização informada.</response>
        /// <response code="404">Organização não foi encontrada.</response>
        /// <response code="500">Erro inesperado.</response>
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
        /// Retorna as informações da organização informada.
        /// </summary>
        /// <param name="sigla">Sigla da organização a qual se deseja obter suas informações.</param>
        /// <returns>Infomações da organização informada.</returns>
        /// <response code="200">Retorna as informações da organização informada.</response>
        /// <response code="404">Organização não foi encontrada.</response>
        /// <response code="500">Erro inesperado.</response>
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
                return NotFound(MensagemErro.ObterMensagem(e));
            }

            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, MensagemErro.ObterMensagem(e));
            }

        }

        /// <summary>
        /// Retorna a organização patriarca da organização informada.
        /// </summary>
        /// <param name="guid">Identificador da organização a qual se deseja obter a sua patriarca.</param>
        /// <returns>Organização patriarca da organização informada.</returns>
        /// <response code="200">Retorna a organização patriarca da organização informada.</response>
        /// <response code="404">Organização não foi encontrada.</response>
        /// <response code="500">Erro inesperado.</response>
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
                return NotFound(MensagemErro.ObterMensagem(e));
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
        /// <response code="500">Erro inesperado..</response>
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

        /// <summary>
        /// Retorna o organograma a partir da organização informada.
        /// </summary>
        /// <param name="guid">Identificador da organização a partir da qual se deseja obter seu organograma.</param>
        /// <param name="filhas">Indica se o organograma irá retornar as organizações filhas e não somente as unidades, o padrão é retornar as organizações filhas.</param>
        /// <returns>Organograma a partir da organização informada.</returns>
        /// <response code="200">Retorna o organograma a partir da organização informada.</response>
        /// <response code="404">Organização não foi encontrada.</response>
        /// <response code="500">Erro inesperado.</response>
        [HttpGet("organograma/{guid}")]
        [ProducesResponseType(typeof(OrganizacaoOrganograma), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult PesquisarOrganograma(string guid, bool filhas = true)
        {
            try
            {
                return new ObjectResult(service.PesquisarOrganograma(guid, filhas));
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
        #endregion

        #region POST
        /// <summary>
        /// Inserção de organizações.
        /// </summary>
        /// <param name="organizacaoPost">Informações da organização a ser inserida.</param>
        /// <returns>Organização recém inserida.</returns>
        /// <response code="201">Retorna a organização recém inserida.</response>
        /// <response code="500">Erro inesperado.</response>
        [HttpPost]
        [Authorize(Policy = "Organizacao.Inserir")]
        [ProducesResponseType(typeof(OrganizacaoModeloGet), 201)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Post([FromBody]OrganizacaoModeloPost organizacaoPost)
        {

            try
            {
                return new ObjectResult(service.Inserir(organizacaoPost));
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

        #endregion

        #region PATCH
        /// <summary>
        /// Alteração de organizações.
        /// </summary>
        /// <param name="id">Identificador da organização a ser alterada.</param>
        /// <param name="organizacao">Informações que serão alteradas da organização.</param>
        /// <response code="200">Organização alterada com sucesso.</response>
        /// <response code="400">Informação inválida.</response>
        /// <response code="404">Recurso não encontrado.</response>
        /// <response code="500">Erro inesperado.</response>
        [HttpPatch("{id}")]
        [Authorize(Policy = "Organizacao.Alterar")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AlterarOrganizacao(int id, [FromBody]OrganizacaoModeloPatch organizacao)
        {
            try
            {
                service.Alterar(id, organizacao);
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

        #endregion

        #region DELETE
        /// <summary>
        /// Exclusão de organizações
        /// </summary>
        /// <param name="id">Identificador da organização a ser excluída.</param>
        /// <returns></returns>
        /// <response code=200>Organização excluída com sucesso.</response>
        /// <response code=500>Erro inesperado</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Organizacao.Excluir")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Excluir(int id)
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
        #endregion
    }
}
