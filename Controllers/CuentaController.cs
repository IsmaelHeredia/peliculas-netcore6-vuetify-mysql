using Microsoft.AspNetCore.Mvc;

namespace NetCorePeliculasSeries.Controllers
{
    public class CuentaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
