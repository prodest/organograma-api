using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Organograma.Infraestrutura.Comum;
using System;
using System.Net;
using Apresentacao.Base;

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
        public void Post([FromBody]string value)
        {
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
