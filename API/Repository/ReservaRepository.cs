using DesignAPI.Data;
using DesignAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using DesignAPI.Models.Entity;

namespace DesignAPI.Repository
{
    public class ReservaRepository : Repository<ReservaEntity>, IReservaRepository
    {
        private readonly TimeSpanConverter _context;

        public ReservaRepository(TimeSpanConverter context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<ReservaEntity>> GetPendientesAsync()
        {
            return await _context.Reservas
                .Where(r => r.Estado == EstadoReserva.Pendiente)
                .OrderBy(r => r.Fecha)
                .ThenBy(r => r.HoraInicio)
                .ToListAsync();
        }

        public async Task<ICollection<ReservaEntity>> GetByFechaAsync(DateTime fecha)
        {
            return await _context.Reservas
                .Where(r => r.Fecha.Date == fecha.Date)
                .OrderBy(r => r.HoraInicio)
                .ToListAsync();
        }

        public async Task<ICollection<ReservaEntity>> GetByCorreoProfesorAsync(string correo)
        {
            return await _context.Reservas
                .Where(r => r.CorreoProfesor == correo)
                .OrderByDescending(r => r.FechaSolicitud)
                .ToListAsync();
        }

        public async Task<ICollection<ReservaEntity>> GetByEstadoAsync(EstadoReserva estado)
        {
            return await _context.Reservas
                .Where(r => r.Estado == estado)
                .OrderByDescending(r => r.Fecha)
                .ToListAsync();
        }

        public async Task<bool> ExisteReservaEnHorarioAsync(DateTime fecha, TimeSpan horaInicio)
        {
            return await _context.Reservas
                .AnyAsync(r => r.Fecha.Date == fecha.Date && r.HoraInicio == horaInicio);
        }


    }
}