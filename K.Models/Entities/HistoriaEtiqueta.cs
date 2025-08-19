using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace K.Models
{

    [Table("HistoriasEtiquetas")]
    public class HistoriaEtiqueta

    {
        [JsonPropertyName("historiaId")]
        public int HistoriaId { get; set; }

        [JsonPropertyName("etiquetaId")]
        public int EtiquetaId { get; set; }

        [JsonIgnore] public virtual Historia Historia { get; set; } = null!;
        [JsonIgnore] public virtual Etiqueta Etiqueta { get; set; } = null!;
    }
}
