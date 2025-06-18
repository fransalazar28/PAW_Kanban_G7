using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace K.Models
{
    public partial class Tablero
    {
        [JsonPropertyName("tableroId")]
        public int TableroId { get; set; }

        [JsonPropertyName("titulo")]
        public string Titulo { get; set; } = null!;

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("usuarioId")]
        public int UsuarioId { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public DateTime FechaCreacion { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Columna> Columnas { get; set; } = new List<Columna>();
    }
}
