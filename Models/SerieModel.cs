namespace NetCorePeliculasSeries.Models
{
    public class SerieModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Comentario { get; set; }
        public string Imagen { get; set; }
        public string Links { get; set; }
        public int Ultima_temporada { get; set; }
        public int Ultimo_capitulo { get; set; }
        public int Estado { get; set; }
        public string Fecha_final_vista { get; set; }
        public string Fecha_registro { get; set; }
        public string Fecha_ultima_actualizacion { get; set; }
    }
}
