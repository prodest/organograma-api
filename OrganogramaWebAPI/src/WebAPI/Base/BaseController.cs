using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Organograma.WebAPI.Base
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
