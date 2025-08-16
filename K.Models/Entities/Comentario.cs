using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace K.Models
{
    [Table("Comentarios")]
    public class Comentario
    {
        [JsonPropertyName("comentarioId")]
        public int ComentarioId { get; set; }

        [JsonPropertyName("historiaId")]
        public int HistoriaId { get; set; }

        [JsonPropertyName("usuarioId")]
        public int UsuarioId { get; set; }

        [Required]
        [JsonPropertyName("texto")]
        public string Texto { get; set; } = null!;

        [JsonPropertyName("fechaRegistro")]
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public virtual Historia Historia { get; set; } = null!;

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
