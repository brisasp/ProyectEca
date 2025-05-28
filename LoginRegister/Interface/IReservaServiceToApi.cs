using LoginRegister.Models;


namespace LoginRegister.Interface
{
    public interface IReservaServiceToApi
    {
        // Obtiene una Reserva desde la API
        Task<IEnumerable<ReservaDTO>> GetReservas();
        // Obtener solo las reservas del usuario autenticado
        Task<IEnumerable<ReservaDTO>> GetMisReservas();
        // Obtener las reservas en estado "Pendiente"
        Task<IEnumerable<ReservaDTO>> GetPendientes();

        // Obtener una reserva por su ID
        Task<ReservaDTO> GetReservaById(int id);
        // Agrega una Reserva a la API
        Task PostReserva(ReservaDTO reserva);

        // Modifica un Dicatador ya existente
        Task PutReserva(ReservaDTO dicatador);

        Task DeleteReserva(int id);
    }
}
