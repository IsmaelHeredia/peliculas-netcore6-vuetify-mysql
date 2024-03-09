using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCorePeliculasSeries.Data;
using NetCorePeliculasSeries.Functions;
using NetCorePeliculasSeries.Models;
using System.Collections;

namespace NetCorePeliculasSeries.Controllers
{
    [Route("api")]
    public class APIController : Controller
    {
        AccesoDatos accesoDatos = new AccesoDatos();
        Seguridad seguridad = new Seguridad();
        PeliculaDatos peliculaDatos = new PeliculaDatos();
        SerieDatos serieDatos = new SerieDatos();
        UsuarioDatos usuarioDatos = new UsuarioDatos();
        Validaciones validacion = new Validaciones();

        [HttpPost("ingreso")]
        public IActionResult Ingreso([FromBody] IngresoModel data)
        {
            string usuario = data.Usuario;
            string clave = data.Clave;

            if (usuario != "" && clave != "")
            {
                if (accesoDatos.Ingreso(data))
                {
                    UsuarioModel usuarioModel = usuarioDatos.ObtenerPorNombre(data.Usuario);
                    string token = seguridad.GenerarTokenJWT(usuarioModel.Id, usuarioModel.Nombre);
                    HttpContext.Session.SetString(new Configuracion().nombre_sesion,token);
                    return Json(new { estado = 1, mensaje = "Ingreso válido", token = token });
                }
                else
                {
                    return Json(new { estado = 0, mensaje = "Ingreso inválido" });
                }
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Faltan datos" });
            }
        }

        [HttpPost("cerrar_sesion")]
        public IActionResult CerrarSesion()
        {
            if (seguridad.ValidarTokenIngreso(Request.Headers["Authorization"].ToString()))
            {
                HttpContext.Session.Remove(new Configuracion().nombre_sesion);
                return Json(new { estado = 1, mensaje = "Ingreso válido" });
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Acceso denegado" });
            }
        }

        [HttpGet("registros")]
        public IActionResult ListarRegistros()
        {
            if (seguridad.ValidarTokenIngreso(Request.Headers["Authorization"].ToString()))
            {
                return Json(new { estado = 1, mensaje = "Peliculas listadas correctamente", peliculas = peliculaDatos.Listar(""), series = serieDatos.Listar("") });
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Acceso denegado" });
            }
        }

        [HttpGet("peliculas")]
        public IActionResult ListarPeliculas()
        {
            if (seguridad.ValidarTokenIngreso(Request.Headers["Authorization"].ToString()))
            {
                return Json(new { estado = 1, mensaje = "Peliculas listadas correctamente", peliculas = peliculaDatos.Listar("") });
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Acceso denegado" });
            }
        }

        [HttpGet("peliculas/{id}")]
        public IActionResult CargarPelicula(int id)
        {
            if (seguridad.ValidarTokenIngreso(Request.Headers["Authorization"].ToString()))
            {
                return Json(new { estado = 1, mensaje = "Pelicula cargada correctamente", pelicula = peliculaDatos.Obtener(id) });
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Acceso denegado" });
            }
        }

        [HttpPost("peliculas")]
        public IActionResult GuardarPelicula([FromBody] PeliculaModel data)
        {
            if (seguridad.ValidarTokenIngreso(Request.Headers["Authorization"].ToString()))
            {
                if (validacion.validarCrearPelicula(data))
                {
                    peliculaDatos.Agregar(data);
                    return Json(new { estado = 1, mensaje = "Pelicula guardada correctamente" });
                }
                else
                {
                    return Json(new { estado = 0, mensaje = "Faltan datos" });
                }
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Acceso denegado" });
            }
        }

        [HttpPut("peliculas/{id}")]
        public IActionResult ActualizarPelicula(int id, [FromBody] PeliculaModel data)
        {
            if (seguridad.ValidarTokenIngreso(Request.Headers["Authorization"].ToString()))
            {
                data.Id = id;
                peliculaDatos.Actualizar(data);
                if (validacion.validarEditarPelicula(data))
                {
                    return Json(new { estado = 1, mensaje = "Pelicula actualizada correctamente" });
                }
                else
                {
                    return Json(new { estado = 0, mensaje = "Faltan datos" });
                }
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Acceso denegado" });
            }
        }

        [HttpDelete("peliculas/{id}")]
        public IActionResult BorrarPelicula(int id)
        {
            if (seguridad.ValidarTokenIngreso(Request.Headers["Authorization"].ToString()))
            {
                PeliculaModel pelicula = new PeliculaModel();
                pelicula.Id = id;
                peliculaDatos.Borrar(pelicula);
                return Json(new { estado = 1, mensaje = "Pelicula borrada correctamente" });
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Acceso denegado" });
            }
        }

        [HttpGet("series")]
        public IActionResult ListarSeries()
        {
            if (seguridad.ValidarTokenIngreso(Request.Headers["Authorization"].ToString()))
            {
                return Json(new { estado = 1, mensaje = "Series listadas correctamente", series = serieDatos.Listar("") });
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Acceso denegado" });
            }
        }

        [HttpGet("series/{id}")]
        public IActionResult CargarSerie(int id)
        {
            if (seguridad.ValidarTokenIngreso(Request.Headers["Authorization"].ToString()))
            {
                return Json(new { estado = 1, mensaje = "Serie cargada correctamente", serie = serieDatos.Obtener(id) });
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Acceso denegado" });
            }
        }

        [HttpPost("series")]
        public IActionResult GuardarSerie([FromBody] SerieModel data)
        {
            if (seguridad.ValidarTokenIngreso(Request.Headers["Authorization"].ToString()))
            {
                if (validacion.validarCrearSerie(data))
                {
                    serieDatos.Agregar(data);
                    return Json(new { estado = 1, mensaje = "Serie guardada correctamente" });
                }
                else
                {
                    return Json(new { estado = 0, mensaje = "Faltan datos" });
                }
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Acceso denegado" });
            }
        }

        [HttpPut("series/{id}")]
        public IActionResult ActualizarSerie(int id, [FromBody] SerieModel data)
        {
            if (seguridad.ValidarTokenIngreso(Request.Headers["Authorization"].ToString()))
            {
                data.Id = id;
                serieDatos.Actualizar(data);
                if (validacion.validarEditarSerie(data))
                {
                    return Json(new { estado = 1, mensaje = "Serie actualizada correctamente" });
                }
                else
                {
                    return Json(new { estado = 0, mensaje = "Faltan datos" });
                }
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Acceso denegado" });
            }
        }

        [HttpDelete("series/{id}")]
        public IActionResult BorrarSerie(int id)
        {
            if (seguridad.ValidarTokenIngreso(Request.Headers["Authorization"].ToString()))
            {
                SerieModel serie = new SerieModel();
                serie.Id = id;
                serieDatos.Borrar(serie);
                return Json(new { estado = 1, mensaje = "Serie borrada correctamente" });
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Acceso denegado" });
            }
        }

        [HttpPost("cuenta")]
        public IActionResult Cuenta([FromBody] CuentaModel data)
        {
            if (seguridad.ValidarTokenIngreso(Request.Headers["Authorization"].ToString()))
            {
                if (validacion.validarActualizarCuenta(data))
                {
                    string usuario_actual = data.Usuario_actual;
                    string nuevo_usuario = data.Nuevo_usuario;
                    string nueva_clave = data.Nueva_clave;
                    string clave_actual = data.Clave_actual;

                    IngresoModel ingresoModel = new IngresoModel();
                    ingresoModel.Usuario = usuario_actual;
                    ingresoModel.Clave = clave_actual;

                    if (accesoDatos.Ingreso(ingresoModel))
                    {
                        int id = usuarioDatos.ObtenerPorNombre(usuario_actual).Id;

                        if (nuevo_usuario != "")
                        {
                            CambiarUsuarioModel cambiarUsuarioModel = new CambiarUsuarioModel();
                            cambiarUsuarioModel.Id = id;
                            cambiarUsuarioModel.Nuevo_usuario = nuevo_usuario;
                            usuarioDatos.ActualizarNombre(cambiarUsuarioModel);
                        }

                        if(nueva_clave != "")
                        {
                            CambiarClaveModel cambiarClaveModel = new CambiarClaveModel();
                            cambiarClaveModel.Id = id;
                            cambiarClaveModel.Nueva_clave = BCrypt.Net.BCrypt.HashPassword(nueva_clave);
                            usuarioDatos.ActualizarClave(cambiarClaveModel);
                        }

                        HttpContext.Session.Remove(new Configuracion().nombre_sesion);

                        return Json(new { estado = 1, mensaje = "Se actualizaron los datos de cuenta correctamente" });
                    }
                    else
                    {
                        return Json(new { estado = 2, mensaje = "Ingreso inválido" });
                    }
                }
                else
                {
                    return Json(new { estado = 0, mensaje = "Faltan datos" });
                }
            }
            else
            {
                return Json(new { estado = 0, mensaje = "Acceso denegado" });
            }
        }

    }
}
