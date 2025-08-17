namespace K.Models.DTOs;

public class BoardDto
{
    public int TableroId { get; set; }
    public string Titulo { get; set; } = null!;
    public List<ColumnDto> Columnas { get; set; } = new();
}

public class ColumnDto
{
    public int ColumnaId { get; set; }
    public string Nombre { get; set; } = null!;
    public List<HistoriaItemDto> Historias { get; set; } = new();
}

public sealed class HistoriaItemDto
{
    public int HistoriaId { get; set; }
    public string Titulo { get; set; } = "";
    public string? Descripcion { get; set; }
    public int Orden { get; set; }
    public int? ResponsableId { get; set; }

    public string? ResponsableNombre { get; set; }
    public DateTime? FechaVencimiento { get; set; }

    public int Comentarios { get; set; } = 0;               
    public List<EtiquetaMiniDto> Etiquetas { get; set; } = new(); 
}

public class EtiquetaMiniDto
{
    public int EtiquetaId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Color { get; set; }
}
