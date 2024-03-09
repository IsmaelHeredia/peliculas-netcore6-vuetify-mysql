using Microsoft.AspNetCore.Mvc;
using NetCorePeliculasSeries.Functions;
using NetCorePeliculasSeries.Models;
using System.Diagnostics;

namespace NetCorePeliculasSeries.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Seguridad seguridad = new Seguridad();
            Configuracion configuracion = new Configuracion();
            string nombre_sesion = configuracion.nombre_sesion;
            string sesion = HttpContext.Session.GetString(nombre_sesion);
            if (seguridad.ValidarSesionJWT(sesion))
            {
                return RedirectToAction("Index", "Administracion");
            }
            else
            {
                return View();
            }
        }
    }
}