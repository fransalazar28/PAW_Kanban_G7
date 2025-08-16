using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace K.Models
{
    public partial class Usuario
    {
        [JsonPropertyName("usuarioId")]
        public int UsuarioId { get; set; }

        [JsonPropertyName("nombreUsuario")]
        public string NombreUsuario { get; set; } = null!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("passwordHash")]
        public string PasswordHash { get; set; } = null!;

        [JsonPropertyName("fechaCreacion")]
        public DateTime FechaCreacion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Tablero> Tableros { get; set; } = new List<Tablero>();

        [JsonIgnore]
        public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();


        [JsonIgnore]
        public virtual ICollection<Historia> HistoriasAsignadas { get; set; } = new List<Historia>();
    }
}
