using LoginRegister.Helpers;
using LoginRegister.Interface;
using LoginRegister.Models;



namespace LoginRegister.Service
{

    public class FranjaHorarioServiceToApi : IFranjaHorarioServiceToApi
    {
        private readonly IHttpJsonProvider<FranjaHorarioDTO> _httpJsonProvider;


        public FranjaHorarioServiceToApi(IHttpJsonProvider<FranjaHorarioDTO> httpJsonProvider)
        {
            _httpJsonProvider = httpJsonProvider;
        }



        public async Task<IEnumerable<FranjaHorarioDTO>> GetFranjas()
        {
            IEnumerable<FranjaHorarioDTO> productos = await _httpJsonProvider.GetAsync(Constants.FRANJA_GET_ALL);

            return productos;
        }

        public async Task<FranjaHorarioDTO> GetFranjaById(int id)
        {

            return await _httpJsonProvider.GetByIdAsync(Constants.FRANJA_GET_BY_ID, id);
        }

        public async Task PostFranja(FranjaHorarioDTO franja)
        {
            try
            {
                if (franja == null) return;
                var response = await _httpJsonProvider.PostAsync(Constants.FRANJA_POST, franja);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear franja: {ex.Message}");
            }
        }

        public async Task PutFranja(FranjaHorarioDTO producto)
        {
            try
            {
                if (producto == null) return;
                string url = string.Format(Constants.FRANJA_PATCH, producto.ID);
                var response = await _httpJsonProvider.PutAsync(url, producto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar franja: {ex.Message}");
            }
        }
        public async Task DeleteFranja(int id)
        {
            try
            {
                var url = string.Format(Constants.FRANJA_GET_BY_ID, id);
                await _httpJsonProvider.Delete(url, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar franja: {ex.Message}");
            }
        }

    }

}