using DesignAPI.Models.Entity;

namespace DesignAPI.Repository.IRepository
{
    public interface IFranjaHorarioRepository : IRepository<FranjaHorarioEntity>
    {
        Task<ICollection<FranjaHorarioEntity>> GetActivosAsync();
        Task<bool> ExistsAsync(TimeSpan horaInicio, TimeSpan horaFin);
        Task<FranjaHorarioEntity?> GetByHoraInicioAsync(TimeSpan horaInicio);

        Task<ICollection<FranjaHorarioEntity>> GetDisponiblesParaFechaAsync(DateTime fecha, List<ReservaEntity> reservas);
    }
}
