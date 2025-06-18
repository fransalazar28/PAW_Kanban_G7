// Comentario.cs
using System;
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

        [JsonPropertyName("texto")]
        public string Texto { get; set; } = null!;

        [JsonPropertyName("fechaRegistro")]
        public DateTime FechaRegistro { get; set; }

        [JsonIgnore]
        public virtual HistoriaUsuario HistoriaUsuario { get; set; } = null!;

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
