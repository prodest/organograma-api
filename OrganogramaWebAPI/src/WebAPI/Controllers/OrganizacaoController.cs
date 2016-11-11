using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Organograma.Infraestrutura.Comum;
using System;
using System.Net;
using Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/organizacoes")]
    public class OrganizacaoController : Controller
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
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message); ;
            }
        }

        // GET api/organizacoes/5
        [HttpGet("{id}")]
        public IActionResult Pesquisar(int id)
        {
            try
            {
                return new ObjectResult(service.Pesquisar(id));
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

        #endregion

        #region POST
        // POST api/organizacoes
        [HttpPost]
        [Authorize]
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
        //[Authorize]
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
        [Authorize]
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
        [Authorize]
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
