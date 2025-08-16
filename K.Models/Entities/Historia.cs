using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace K.Models
{

    [Table("HistoriasUsuario")]
    public class Historia
    {
        [Key]
        [JsonPropertyName("historiaId")]
        public int HistoriaId { get; set; }

        [Required, MaxLength(150)]
        [JsonPropertyName("titulo")]
        public string Titulo { get; set; } = string.Empty;

        [MaxLength(4000)]
        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("columnaId")]
        public int ColumnaId { get; set; }


        [JsonPropertyName("orden")]
        public int Orden { get; set; } = int.MaxValue;

        [JsonPropertyName("responsableId")]
        public int? ResponsableId { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("fechaVencimiento")]
        public DateTime? FechaVencimiento { get; set; }


        [ForeignKey(nameof(ResponsableId))]
        [JsonIgnore]
        public virtual Usuario? Responsable { get; set; }

        [ForeignKey(nameof(ColumnaId))]
        [JsonIgnore]
        public virtual Columna Columna { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

        [JsonIgnore]
        public virtual ICollection<HistoriaEtiqueta> HistoriasEtiquetas { get; set; } = new List<HistoriaEtiqueta>();
    }
}
