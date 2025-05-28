using DesignAPI.Data;
using DesignAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using DesignAPI.Models.Entity;

namespace DesignAPI.Repository
{
    public class FranjaHorarioRepository : Repository<FranjaHorarioEntity>, IFranjaHorarioRepository
    {
        private readonly TimeSpanConverter _context;

        public FranjaHorarioRepository(TimeSpanConverter context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<FranjaHorarioEntity>> GetActivosAsync()
        {
            return await _context.FranjasHorarios
                .Where(f => f.Activa)
                .OrderBy(f => f.HoraInicio)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(TimeSpan horaInicio, TimeSpan horaFin)
        {
            return await _context.FranjasHorarios.AnyAsync(f =>
                f.HoraInicio == horaInicio && f.HoraFin == horaFin);
        }

        public async Task<FranjaHorarioEntity?> GetByHoraInicioAsync(TimeSpan horaInicio)
        {
            return await _context.FranjasHorarios
                .FirstOrDefaultAsync(f => f.HoraInicio == horaInicio);
        }

        public async Task<ICollection<FranjaHorarioEntity>> GetDisponiblesParaFechaAsync(DateTime fecha, List<ReservaEntity> reservas)
        {
            var reservadas = reservas
                .Where(r => r.Fecha.Date == fecha.Date)
                .Select(r => r.HoraInicio)
                .ToList();

            return await _context.FranjasHorarios
                .Where(f => !reservadas.Contains(f.HoraInicio) && f.Activa)
                .OrderBy(f => f.HoraInicio)
                .ToListAsync();
        }
    }
}