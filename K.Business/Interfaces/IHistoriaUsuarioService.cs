using System.Collections.Generic;
using System.Threading.Tasks;
using K.Business.Dtos;
using K.Business.Requests;

namespace K.Business.Interfaces
{
    public interface IHistoriaUsuarioService
    {
        Task<IEnumerable<HistoriaUsuarioDto>> ListarPorTableroAsync(int tableroId);
        Task<HistoriaUsuarioDto> CrearHistoriaAsync(CrearHistoriaRequest request);
        Task MoverHistoriaAsync(MoverHistoriaRequest request);
        Task EliminarHistoriaAsync(int historiaId);
    }
}