using LoginRegister.Models;


namespace LoginRegister.Interface
{
    public interface IDiaNoLectivoServiceToApi
    {
        // Obtiene una Reserva desde la API
        Task<IEnumerable<DiaNoLectivoDTO>> GetDiasNoLectivos();

        // Obtener un día no lectivo por ID
        Task<DiaNoLectivoDTO> GetDiaNoLectivoById(int id);

        // Agrega una Reserva a la API
        Task PostDiaNoLectivo(DiaNoLectivoDTO dia);

        // Modifica un Dicatador ya existente
        Task PutDiaNoLectivo(DiaNoLectivoDTO dia);

        // Eliminar un día no lectivo por ID
        Task DeleteDiaNoLectivo(int id);
    }
}
