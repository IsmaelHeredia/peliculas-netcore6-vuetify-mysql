using MySql.Data.MySqlClient;
using NetCorePeliculasSeries.Functions;
using NetCorePeliculasSeries.Models;

namespace NetCorePeliculasSeries.Data
{
    public class SerieDatos
    {
        string connection_string = new Configuracion().conexion_string;

        public List<SerieModel> Listar(string patron)
        {
            var series = new List<SerieModel>();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,nombre,comentario,imagen,links,ultima_temporada,ultimo_capitulo,estado,fecha_final_vista,fecha_registro,fecha_ultima_actualizacion FROM series s WHERE s.nombre LIKE @patron", connection);
                query.Parameters.AddWithValue("@patron", "%" + patron + "%");

                var dr = query.ExecuteReader();

                while (dr.Read())
                {
                    series.Add(
                        new SerieModel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Nombre = Convert.ToString(dr["nombre"]),
                            Comentario = Convert.ToString(dr["comentario"]),
                            Imagen = Convert.ToString(dr["imagen"]),
                            Links = Convert.ToString(dr["links"]),
                            Ultima_temporada = Convert.ToInt32(dr["ultima_temporada"]),
                            Ultimo_capitulo = Convert.ToInt32(dr["ultimo_capitulo"]),
                            Estado = Convert.ToInt32(dr["estado"]),
                            Fecha_final_vista = Convert.ToString(dr["fecha_final_vista"]),
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

            return series;
        }

        public SerieModel Obtener(int id_serie)
        {
            var proveedor = new SerieModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,nombre,comentario,imagen,links,ultima_temporada,ultimo_capitulo,estado,fecha_final_vista,fecha_registro,fecha_ultima_actualizacion FROM series s WHERE s.id = @id_serie", connection);
                query.Parameters.AddWithValue("@id_serie", id_serie);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    proveedor = new SerieModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Comentario = Convert.ToString(dr["comentario"]),
                        Imagen = Convert.ToString(dr["imagen"]),
                        Links = Convert.ToString(dr["links"]),
                        Ultima_temporada = Convert.ToInt32(dr["ultima_temporada"]),
                        Ultimo_capitulo = Convert.ToInt32(dr["ultimo_capitulo"]),
                        Estado = Convert.ToInt32(dr["estado"]),
                        Fecha_final_vista = Convert.ToString(dr["fecha_final_vista"]),
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

        public bool Agregar(SerieModel serie)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                string fecha_hoy_ahora = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

                string fecha_registro = fecha_hoy_ahora;
                string fecha_ultima_actualizacion = fecha_hoy_ahora;

                string ruta_imagen = "";

                if (serie.Imagen != "")
                {
                    Archivos archivos = new Archivos();
                    ruta_imagen = archivos.subirArchivo(serie.Imagen);
                }

                var query = new MySqlCommand("INSERT INTO series(nombre,comentario,imagen,links,ultima_temporada,ultimo_capitulo,estado,fecha_final_vista,fecha_registro,fecha_ultima_actualizacion) VALUES(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", connection);
                query.Parameters.AddWithValue("@p1", serie.Nombre);
                query.Parameters.AddWithValue("@p2", serie.Comentario);
                query.Parameters.AddWithValue("@p3", ruta_imagen);
                query.Parameters.AddWithValue("@p4", serie.Links);
                query.Parameters.AddWithValue("@p5", serie.Ultima_temporada);
                query.Parameters.AddWithValue("@p6", serie.Ultimo_capitulo);
                query.Parameters.AddWithValue("@p7", serie.Estado);
                query.Parameters.AddWithValue("@p8", serie.Fecha_final_vista);
                query.Parameters.AddWithValue("@p9", fecha_registro);
                query.Parameters.AddWithValue("@p10", fecha_ultima_actualizacion);

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

        public bool Actualizar(SerieModel serie)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                string fecha_ultima_actualizacion = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

                string ruta_imagen = "";

                if (serie.Imagen != "")
                {
                    Archivos archivos = new Archivos();
                    archivos.borrarArchivo(this.Obtener(serie.Id).Imagen);
                    ruta_imagen = archivos.subirArchivo(serie.Imagen);
                }

                var query = new MySqlCommand("UPDATE series SET nombre = @p1, comentario = @p2, imagen = @p3, links = @p4, ultima_temporada = @p5, ultimo_capitulo = @p6, estado = @p7, fecha_final_vista = @p8, fecha_ultima_actualizacion = @p9 WHERE id = @p10", connection);
                query.Parameters.AddWithValue("@p1", serie.Nombre);
                query.Parameters.AddWithValue("@p2", serie.Comentario);
                query.Parameters.AddWithValue("@p3", ruta_imagen);
                query.Parameters.AddWithValue("@p4", serie.Links);
                query.Parameters.AddWithValue("@p5", serie.Ultima_temporada);
                query.Parameters.AddWithValue("@p6", serie.Ultimo_capitulo);
                query.Parameters.AddWithValue("@p7", serie.Estado);
                query.Parameters.AddWithValue("@p8", serie.Fecha_final_vista);
                query.Parameters.AddWithValue("@p9", fecha_ultima_actualizacion);
                query.Parameters.AddWithValue("@p10", serie.Id);

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

        public bool Borrar(SerieModel serie)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                Archivos archivos = new Archivos();
                archivos.borrarArchivo(this.Obtener(serie.Id).Imagen);

                var query = new MySqlCommand("DELETE FROM series WHERE id = @p1", connection);
                query.Parameters.AddWithValue("@p1", serie.Id);

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
