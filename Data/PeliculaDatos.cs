using MySql.Data.MySqlClient;
using NetCorePeliculasSeries.Models;
using NetCorePeliculasSeries.Functions;

namespace NetCorePeliculasSeries.Data
{
    public class PeliculaDatos
    {
        string connection_string = new Configuracion().conexion_string;

        public List<PeliculaModel> Listar(string patron)
        {
            var peliculas = new List<PeliculaModel>();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,nombre,comentario,imagen,links,estado,fecha_vista,fecha_registro,fecha_ultima_actualizacion FROM peliculas p WHERE p.nombre LIKE @patron", connection);
                query.Parameters.AddWithValue("@patron", "%" + patron + "%");

                var dr = query.ExecuteReader();

                while (dr.Read())
                {
                    peliculas.Add(
                        new PeliculaModel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Nombre = Convert.ToString(dr["nombre"]),
                            Comentario = Convert.ToString(dr["comentario"]),
                            Imagen = Convert.ToString(dr["imagen"]),
                            Links = Convert.ToString(dr["links"]),
                            Estado = Convert.ToInt32(dr["estado"]),
                            Fecha_vista = Convert.ToString(dr["fecha_vista"]),
                            Fecha_registro = Convert.ToString(dr["fecha_registro"]),
                            Fecha_ultima_actualizacion = Convert.ToString(dr["fecha_ultima_actualizacion"])
                        }
                    );
                }

                dr.Close();

                connection.Close();
                connection.Dispose();

            }
            catch
            {
                throw;
            }

            return peliculas;
        }

        public PeliculaModel Obtener(int id_pelicula)
        {
            var proveedor = new PeliculaModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,nombre,comentario,imagen,links,estado,fecha_vista,fecha_registro,fecha_ultima_actualizacion FROM peliculas p WHERE p.id = @id_pelicula", connection);
                query.Parameters.AddWithValue("@id_pelicula", id_pelicula);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    proveedor = new PeliculaModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Comentario = Convert.ToString(dr["comentario"]),
                        Imagen = Convert.ToString(dr["imagen"]),
                        Links = Convert.ToString(dr["links"]),
                        Estado = Convert.ToInt32(dr["estado"]),
                        Fecha_vista = Convert.ToString(dr["fecha_vista"]),
                        Fecha_registro = Convert.ToString(dr["fecha_registro"]),
                        Fecha_ultima_actualizacion = Convert.ToString(dr["fecha_ultima_actualizacion"])
                    };
                }
                else
                {
                    return null;
                }

                dr.Close();

                connection.Close();
                connection.Dispose();

            }
            catch
            {
                throw;
            }

            return proveedor;
        }

        public bool Agregar(PeliculaModel pelicula)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                string fecha_hoy_ahora = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

                string fecha_registro = fecha_hoy_ahora;
                string fecha_ultima_actualizacion = fecha_hoy_ahora;

                string ruta_imagen = "";

                if (pelicula.Imagen != "")
                {
                    Archivos archivos = new Archivos();
                    ruta_imagen = archivos.subirArchivo(pelicula.Imagen);
                }

                var query = new MySqlCommand("INSERT INTO peliculas(nombre,comentario,imagen,links,estado,fecha_vista,fecha_registro,fecha_ultima_actualizacion) VALUES(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", connection);
                query.Parameters.AddWithValue("@p1", pelicula.Nombre);
                query.Parameters.AddWithValue("@p2", pelicula.Comentario);
                query.Parameters.AddWithValue("@p3", ruta_imagen);
                query.Parameters.AddWithValue("@p4", pelicula.Links);
                query.Parameters.AddWithValue("@p5", pelicula.Estado);
                query.Parameters.AddWithValue("@p6", pelicula.Fecha_vista);
                query.Parameters.AddWithValue("@p7", fecha_registro);
                query.Parameters.AddWithValue("@p8", fecha_ultima_actualizacion);

                query.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();

                return true;
            }
            catch
            {
                //return false;
                throw;
            }
        }

        public bool Actualizar(PeliculaModel pelicula)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                string fecha_ultima_actualizacion = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

                string ruta_imagen = "";

                if (pelicula.Imagen != "")
                {
                    Archivos archivos = new Archivos();
                    archivos.borrarArchivo(this.Obtener(pelicula.Id).Imagen);
                    ruta_imagen = archivos.subirArchivo(pelicula.Imagen);
                }

                var query = new MySqlCommand("UPDATE peliculas SET nombre = @p1, comentario = @p2, imagen = @p3, links = @p4, estado = @p5, fecha_vista = @p6, fecha_ultima_actualizacion = @p7 WHERE id = @p8", connection);
                query.Parameters.AddWithValue("@p1", pelicula.Nombre);
                query.Parameters.AddWithValue("@p2", pelicula.Comentario);
                query.Parameters.AddWithValue("@p3", ruta_imagen);
                query.Parameters.AddWithValue("@p4", pelicula.Links);
                query.Parameters.AddWithValue("@p5", pelicula.Estado);
                query.Parameters.AddWithValue("@p6", pelicula.Fecha_vista);
                query.Parameters.AddWithValue("@p7", fecha_ultima_actualizacion);
                query.Parameters.AddWithValue("@p8", pelicula.Id);

                query.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Borrar(PeliculaModel pelicula)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                Archivos archivos = new Archivos();
                archivos.borrarArchivo(this.Obtener(pelicula.Id).Imagen);

                var query = new MySqlCommand("DELETE FROM peliculas WHERE id = @p1", connection);
                query.Parameters.AddWithValue("@p1", pelicula.Id);

                query.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
