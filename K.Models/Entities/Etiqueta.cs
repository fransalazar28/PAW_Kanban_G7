using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace K.Models
{
    [Table("Etiquetas")]
    public class Etiqueta
    {
        [JsonPropertyName("etiquetaId")]
        public int EtiquetaId { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [JsonPropertyName("color")]
        public string? Color { get; set; }

        [JsonIgnore]
        public virtual ICollection<HistoriaEtiqueta> HistoriasEtiquetas { get; set; } = new List<HistoriaEtiqueta>();
    }
}
