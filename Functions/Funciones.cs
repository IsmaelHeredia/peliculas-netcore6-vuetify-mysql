namespace NetCorePeliculasSeries.Functions
{
    public class Funciones
    {
        public bool ValidarBase64(string Imagen_base64)
        {
            if (Imagen_base64.Contains(";base64,"))
            {
                string[] split_contenido = Imagen_base64.Split(";base64,");
                string contenido = split_contenido[1];
                Span<byte> span_byte = new Span<byte>(new byte[contenido.Length]);
                return Convert.TryFromBase64String(contenido, span_byte, out int bytesParsed);
            }
            else
            {
                return false;
            }
        }
    }
}
