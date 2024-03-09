using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace NetCorePeliculasSeries.Functions
{
    public class Archivos
    {
        public string subirArchivo(string Imagen_base64)
        {
            string nombreGenerado = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".jpg";
            string rutaFinal = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\", nombreGenerado);

            string[] split_contenido = Imagen_base64.Split(";base64,");
            string contenido_base64 = split_contenido[1];

            byte[] bytes = Convert.FromBase64String(contenido_base64);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image pic = Image.FromStream(ms);
                pic.Save(rutaFinal);
            }

            return nombreGenerado;
        }

        public bool borrarArchivo(string nombreArchivo)
        {
            string rutaFinal = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\", nombreArchivo);
            try
            {
                if (File.Exists(rutaFinal))
                {
                    File.Delete(rutaFinal);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
