using System.ComponentModel.DataAnnotations;

namespace K.Models
{
    public class WidgetConfig
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Tipo { get; set; }

        public string UrlApi { get; set; }

        public string? ApiKey { get; set; }

        public int TiempoRefresco { get; set; }

        public string Tamaño { get; set; }

        public string Posicion { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        public string Visibilidad { get; set; } // "publico", "privado" o "grupal"

        public bool Favorito { get; set; }

        public bool Oculto { get; set; }
    }
}
