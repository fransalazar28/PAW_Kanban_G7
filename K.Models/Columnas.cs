// Columna.cs
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace K.Models
{
    public class Columna
    {
        [JsonPropertyName("columnaId")]
        public int ColumnaId { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [JsonPropertyName("orden")]
        public int Orden { get; set; }

        [JsonPropertyName("tableroId")]
        public int TableroId { get; set; }

        [JsonIgnore]
        public virtual Tablero Tablero { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<HistoriaUsuario> HistoriasUsuario { get; set; } = new HashSet<HistoriaUsuario>();
    }
}
