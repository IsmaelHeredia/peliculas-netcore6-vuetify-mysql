using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using NetCorePeliculasSeries.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using NetCorePeliculasSeries.Functions;

namespace NetCorePeliculasSeries.Data
{
    public class UsuarioDatos
    {
        string connection_string = new Configuracion().conexion_string;

        public UsuarioModel ObtenerPorId(int id_usuario)
        {
            var usuario = new UsuarioModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,nombre,clave,fecha_registro FROM usuarios u WHERE u.id = @id_usuario", connection);
                query.Parameters.AddWithValue("@id_usuario", id_usuario);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    usuario = new UsuarioModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Clave = Convert.ToString(dr["clave"]),
                        Fecha_registro = Convert.ToString(dr["fecha_registro"])
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

            return usuario;
        }

        public UsuarioModel ObtenerPorNombre(string nombre)
        {
            var usuario = new UsuarioModel();

            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("SELECT id,nombre,clave,fecha_registro FROM usuarios u WHERE u.nombre = @nombre", connection);
                query.Parameters.AddWithValue("@nombre", nombre);

                var dr = query.ExecuteReader();

                dr.Read();

                if (dr.HasRows)
                {
                    usuario = new UsuarioModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Nombre = Convert.ToString(dr["nombre"]),
                        Clave = Convert.ToString(dr["clave"]),
                        Fecha_registro = Convert.ToString(dr["fecha_registro"])
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

            return usuario;
        }

        public bool ActualizarNombre(CambiarUsuarioModel usuario)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("UPDATE usuarios SET nombre = @p1 WHERE id = @p2", connection);
                query.Parameters.AddWithValue("@p1", usuario.Nuevo_usuario);
                query.Parameters.AddWithValue("@p2", usuario.Id);

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

        public bool ActualizarClave(CambiarClaveModel usuario)
        {
            try
            {
                var connection = new MySqlConnection(connection_string);
                connection.Open();

                var query = new MySqlCommand("UPDATE usuarios SET clave = @p1 WHERE id = @p2", connection);
                query.Parameters.AddWithValue("@p1", usuario.Nueva_clave);
                query.Parameters.AddWithValue("@p2", usuario.Id);

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
