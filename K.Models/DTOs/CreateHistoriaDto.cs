namespace K.Models.DTOs;

public class CreateHistoriaDto
{
    public string Titulo { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int ColumnaId { get; set; }
    public int? ResponsableId { get; set; }
}
