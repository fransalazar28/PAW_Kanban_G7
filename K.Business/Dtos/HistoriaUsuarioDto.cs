namespace K.Business.Dtos
{
    public class HistoriaUsuarioDto
    {
        public int HistoriaId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int ColumnaId { get; set; }
        public int TableroId { get; set; }
        public int? ResponsableId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }
}