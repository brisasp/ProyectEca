using LoginRegister.Helpers;
using LoginRegister.Interface;
using LoginRegister.Models;
using System.Security.Cryptography;



namespace LoginRegister.Service
{

    public class DiaNoLectivoServiceToApi : IDiaNoLectivoServiceToApi
    {
        private readonly IHttpJsonProvider<DiaNoLectivoDTO> _httpJsonProvider;


        public DiaNoLectivoServiceToApi(IHttpJsonProvider<DiaNoLectivoDTO> httpJsonProvider)
        {
            _httpJsonProvider = httpJsonProvider;
        }

        public async Task<IEnumerable<DiaNoLectivoDTO>> GetDiasNoLectivos()
        {
            try
            {
                Console.WriteLine("?? Llamando al endpoint DIA_NO_LECTIVO_GET_ALL");
                var result = await _httpJsonProvider.GetAsync(Constants.DIA_NO_LECTIVO_GET_ALL);
                return result ?? Enumerable.Empty<DiaNoLectivoDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"?? Error en GetDiasNoLectivos: {ex.Message}");
                return null!;
            }
          
        }

        public async Task<DiaNoLectivoDTO> GetDiaNoLectivoById(int id)
        {

            return await _httpJsonProvider.GetByIdAsync(Constants.DIA_NO_LECTIVO_GET_BY_ID, id);

        }

        public async Task PostDiaNoLectivo(DiaNoLectivoDTO producto)
        {
            try
            {
                if (producto == null) return;
                var response = await _httpJsonProvider.PostAsync(Constants.DIA_NO_LECTIVO_POST, producto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear día no lectivo: {ex.Message}");
            }
        }

        public async Task PutDiaNoLectivo(DiaNoLectivoDTO producto)
        {
            try
            {
                string url = string.Format(Constants.DIA_NO_LECTIVO_PATCH, producto.ID);
                await _httpJsonProvider.PutAsync(url, producto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar día no lectivo: {ex.Message}");
            }
        }
        public async Task DeleteDiaNoLectivo(int id)
        {
            try
            {            
               await _httpJsonProvider.Delete(Constants.DIA_NO_LECTIVO_GET_BY_ID, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar día no lectivo: {ex.Message}");
            }
        }

    }

}