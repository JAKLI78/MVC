using System.Web.Mvc;
using MVCTask.Filters;

namespace MVCTask.Controllers
{    
    [LogResultFilter]
    [LogActionFilter]
    [LogAuthorizationFilter]
    [LogExceptionFilter]
    public class BaseController : Controller
    {
    }
}