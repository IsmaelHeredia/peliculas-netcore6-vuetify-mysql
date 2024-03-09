using Microsoft.AspNetCore.Mvc;
using NetCorePeliculasSeries.Functions;

namespace NetCorePeliculasSeries.Controllers
{
    public class AdministracionController : Controller
    {
        [HttpGet("administracion")]
        public IActionResult Index()
        {
            Seguridad seguridad = new Seguridad();
            Configuracion configuracion = new Configuracion();
            string nombre_sesion = configuracion.nombre_sesion;
            string sesion = HttpContext.Session.GetString(nombre_sesion);
            if (seguridad.ValidarSesionJWT(sesion))
            {
                ViewBag.dir = System.Environment.CurrentDirectory;
                ViewBag.ses = HttpContext.Session.GetString("app_login");
                ViewBag.usuario_logeado = seguridad.cargarNombreToken(sesion);
                return View();
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
