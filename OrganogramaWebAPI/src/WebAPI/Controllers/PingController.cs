using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Organograma.WebAPI.Controllers
{
    [Route("api/ping")]
    public class PingController
    {
        /// <summary>
        /// Health check da api.
        /// </summary>
        /// <returns>Pong.</returns>
        /// <response code="200">Retorna pong.</response>
        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Get()
        {
            return new ObjectResult("pong");
        }
    }
}
