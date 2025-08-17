using System.ComponentModel.DataAnnotations;

namespace K.Models.DTOs
{
    public record MoveHistoriaDto(
        [Required] int ColumnaId,
        [Range(0, int.MaxValue)] int NewIndex
    );
}
