using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Organograma.Infraestrutura.Comum;
using System;
using System.Net;
using Apresentacao.Base;
using Organograma.Apresentacao.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Organograma.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrganizacaoController : Controller
    {

        private IOrganizacaoWorkService service;

        public OrganizacaoController(IOrganizacaoWorkService service)
        {
            this.service = service;
        }
        
        // GET: api/organizacao
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return new ObjectResult(service.Listar());
                //return new ObjectResult("Get");
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

        // GET api/organizacao/5
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

        // POST api/organizacao
        [HttpPost]
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

            catch(DbUpdateException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
