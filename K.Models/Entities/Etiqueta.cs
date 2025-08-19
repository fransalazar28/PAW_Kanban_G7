using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace K.Models
{
    [Table("Etiquetas")]
    public class Etiqueta
    {
        [JsonPropertyName("etiquetaId")]
        public int EtiquetaId { get; set; }

        [Required, MaxLength(50)]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [MaxLength(20)]
        [JsonPropertyName("color")]
        public string? Color { get; set; }

        public int TableroId { get; set; }

        [JsonIgnore] public virtual Tablero Tablero { get; set; } = null!;


        [JsonIgnore]
        public virtual ICollection<HistoriaEtiqueta> HistoriasEtiquetas { get; set; }
            = new List<HistoriaEtiqueta>();
    }
}
