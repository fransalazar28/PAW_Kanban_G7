namespace K.Business.Requests
{
    public class CrearHistoriaRequest
    {
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int ColumnaId { get; set; }
        public int? ResponsableId { get; set; }
    }
}
