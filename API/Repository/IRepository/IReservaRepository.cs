using DesignAPI.Models.Entity;

namespace DesignAPI.Repository.IRepository
{
    public interface IReservaRepository : IRepository<ReservaEntity>
    {
        Task<ICollection<ReservaEntity>> GetPendientesAsync();
        Task<ICollection<ReservaEntity>> GetByFechaAsync(DateTime fecha);
        Task<ICollection<ReservaEntity>> GetByCorreoProfesorAsync(string correo);
        Task<ICollection<ReservaEntity>> GetByEstadoAsync(EstadoReserva estado);
        Task<bool> ExisteReservaEnHorarioAsync(DateTime fecha, TimeSpan horaInicio);
    }
}
