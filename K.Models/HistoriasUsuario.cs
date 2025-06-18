// HistoriaUsuario.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace K.Models
{
    [Table("HistoriasUsuario")]
    public class HistoriaUsuario
    {
        [JsonPropertyName("historiaId")]
        public int HistoriaId { get; set; }

        [JsonPropertyName("titulo")]
        public string Titulo { get; set; } = null!;

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; } = null!;

        [JsonPropertyName("columnaId")]
        public int ColumnaId { get; set; }

        [JsonPropertyName("responsableId")]
        public int? ResponsableId { get; set; }

        // Propiedad de navegación para el usuario responsable
        [ForeignKey(nameof(ResponsableId))]
        [JsonIgnore]
        public virtual Usuario? Responsable { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime FechaCreacion { get; set; }

        [JsonPropertyName("fechaVencimiento")]
        public DateTime? FechaVencimiento { get; set; }

        [JsonIgnore]
        public virtual Columna Columna { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

        [JsonIgnore]
        public virtual ICollection<HistoriaEtiqueta> HistoriasEtiquetas { get; set; } = new List<HistoriaEtiqueta>();
    }
}
