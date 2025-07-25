﻿// HistoriaEtiqueta.cs
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace K.Models
{
    [Table("HistoriaEtiqueta")]
    public class HistoriaEtiqueta
    {
        [JsonPropertyName("historiaId")]
        public int HistoriaId { get; set; }

        [JsonPropertyName("etiquetaId")]
        public int EtiquetaId { get; set; }

        [JsonIgnore]
        public virtual HistoriaUsuario HistoriaUsuario { get; set; } = null!;

        [JsonIgnore]
        public virtual Etiqueta Etiqueta { get; set; } = null!;
    }
}
