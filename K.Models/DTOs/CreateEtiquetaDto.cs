namespace K.Models.DTOs
{
    public class CreateEtiquetaDto
    {
        public int TableroId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Color { get; set; }
    }
}
