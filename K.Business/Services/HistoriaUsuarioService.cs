using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using K.Business.Dtos;
using K.Business.Interfaces;
using K.Business.Requests;
using K.Data.MSSql;
using K.Data.Repositories.Interfaces;
using K.Models.Entities;

namespace K.Business.Services
{
    public class HistoriaUsuarioService : IHistoriaUsuarioService
    {
        private readonly IGenericRepository<HistoriaUsuario> _historiaRepo;
        private readonly IGenericRepository<Columna> _columnaRepo;
        private readonly IGenericRepository<Tablero> _tableroRepo;

        public HistoriaUsuarioService(
            IGenericRepository<HistoriaUsuario> historiaRepo,
            IGenericRepository<Columna> columnaRepo,
            IGenericRepository<Tablero> tableroRepo)
        {
            _historiaRepo = historiaRepo;
            _columnaRepo = columnaRepo;
            _tableroRepo = tableroRepo;
        }

        public async Task<IEnumerable<HistoriaUsuarioDto>> ListarPorTableroAsync(int tableroId)
        {
            // Validación de existencia de tablero
            var tablero = await _tableroRepo.GetByIdAsync(tableroId)
                ?? throw new KeyNotFoundException($"Tablero {tableroId} no encontrado");

            // Listar todas las historias de usuario y filtrar por tablero
            var allHistorias = await _historiaRepo.ListAsync();
            var filtradas = allHistorias
                .Where(h => h.Columna.TableroId == tableroId)
                .Select(h => new HistoriaUsuarioDto
                {
                    HistoriaId = h.HistoriaId,
                    Titulo = h.Titulo,
                    Descripcion = h.Descripcion,
                    ColumnaId = h.ColumnaId,
                    TableroId = h.Columna.TableroId,
                    ResponsableId = h.ResponsableId,
                    FechaCreacion = h.FechaCreacion,
                    FechaVencimiento = h.FechaVencimiento
                });

            return filtradas;
        }

        public async Task<HistoriaUsuarioDto> CrearHistoriaAsync(CrearHistoriaRequest request)
        {
            // Validar columna
            var columna = await _columnaRepo.GetByIdAsync(request.ColumnaId)
                ?? throw new KeyNotFoundException($"Columna {request.ColumnaId} no encontrada");

            var entidad = new HistoriaUsuario
            {
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                ColumnaId = request.ColumnaId,
                ResponsableId = request.ResponsableId,
                FechaCreacion = DateTime.UtcNow
            };
            await _historiaRepo.AddAsync(entidad);

            return new HistoriaUsuarioDto
            {
                HistoriaId = entidad.HistoriaId,
                Titulo = entidad.Titulo,
                Descripcion = entidad.Descripcion,
                ColumnaId = entidad.ColumnaId,
                TableroId = columna.TableroId,
                ResponsableId = entidad.ResponsableId,
                FechaCreacion = entidad.FechaCreacion,
                FechaVencimiento = entidad.FechaVencimiento
            };
        }

        public async Task MoverHistoriaAsync(MoverHistoriaRequest request)
        {
            var historia = await _historiaRepo.GetByIdAsync(request.HistoriaId)
                ?? throw new KeyNotFoundException($"Historia {request.HistoriaId} no encontrada");
            var columnaDestino = await _columnaRepo.GetByIdAsync(request.ColumnaDestinoId)
                ?? throw new KeyNotFoundException($"Columna destino {request.ColumnaDestinoId} no encontrada");

            if (historia.ColumnaId == request.ColumnaDestinoId)
                return;

            historia.ColumnaId = request.ColumnaDestinoId;
            await _historiaRepo.UpdateAsync(historia);
        }

        public async Task EliminarHistoriaAsync(int historiaId)
        {
            var historia = await _historiaRepo.GetByIdAsync(historiaId)
                ?? throw new KeyNotFoundException($"Historia {historiaId} no encontrada");
            await _historiaRepo.DeleteAsync(historia);
        }
    }
}
