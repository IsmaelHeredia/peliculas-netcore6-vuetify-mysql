using NetCorePeliculasSeries.Models;

namespace NetCorePeliculasSeries.Functions
{
    public class Validaciones
    {
        public bool validarCrearPelicula(PeliculaModel pelicula)
        {
            bool respuesta = true;

            if (string.IsNullOrEmpty(pelicula.Nombre))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(pelicula.Estado)))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(pelicula.Fecha_vista))
            {
                respuesta = false;
            }

            return respuesta;
        }

        public bool validarEditarPelicula(PeliculaModel pelicula)
        {
            bool respuesta = true;

            if (string.IsNullOrEmpty(pelicula.Nombre))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(pelicula.Estado)))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(pelicula.Fecha_vista))
            {
                respuesta = false;
            }

            return respuesta;
        }

        public bool validarCrearSerie(SerieModel serie)
        {
            bool respuesta = true;

            if (string.IsNullOrEmpty(serie.Nombre))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(serie.Ultimo_capitulo)))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(serie.Ultima_temporada)))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(serie.Estado)))
            {
                respuesta = false;
            }

            return respuesta;
        }

        public bool validarEditarSerie(SerieModel serie)
        {
            bool respuesta = true;

            if (string.IsNullOrEmpty(serie.Nombre))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(serie.Ultimo_capitulo)))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(serie.Ultima_temporada)))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(serie.Estado)))
            {
                respuesta = false;
            }

            return respuesta;
        }

        public bool validarActualizarCuenta(CuentaModel actualizarCuenta)
        {
            bool respuesta = true;

            if (string.IsNullOrEmpty(actualizarCuenta.Usuario_actual))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(actualizarCuenta.Clave_actual))
            {
                respuesta = false;
            }

            return respuesta;
        }

        public bool validarCambiarUsuario(CambiarUsuarioModel cambiarUsuario)
        {
            bool respuesta = true;

            if (string.IsNullOrEmpty(Convert.ToString(cambiarUsuario.Id)))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(cambiarUsuario.Usuario_actual))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(cambiarUsuario.Clave_actual))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(cambiarUsuario.Nuevo_usuario))
            {
                respuesta = false;
            }

            return respuesta;
        }

        public bool validarCambiarClave(CambiarClaveModel cambiarClave)
        {
            bool respuesta = true;

            if (string.IsNullOrEmpty(Convert.ToString(cambiarClave.Id)))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(cambiarClave.Usuario_actual))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(cambiarClave.Clave_actual))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(cambiarClave.Nueva_clave))
            {
                respuesta = false;
            }

            return respuesta;
        }
    }
}
