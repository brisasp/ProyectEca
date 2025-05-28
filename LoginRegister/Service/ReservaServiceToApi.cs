using LoginRegister.Helpers;
using LoginRegister.Interface;
using LoginRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LoginRegister.Service
{
    public class ReservaServiceToApi : IReservaServiceToApi
    {
        private readonly IHttpJsonProvider<ReservaDTO> _httpJsonProvider;


        public ReservaServiceToApi(IHttpJsonProvider<ReservaDTO> httpJsonProvider)
        {
            _httpJsonProvider = httpJsonProvider;
        }



        public async Task<IEnumerable<ReservaDTO>> GetReservas()
        {

            return await _httpJsonProvider.GetAsync(Constants.RESERVA_GET_ALL);
        }

        public async Task<IEnumerable<ReservaDTO>> GetMisReservas()
        {

            return await _httpJsonProvider.GetAsync(Constants.RESERVA_MIS_RESERVAS);
        }

        public async Task<IEnumerable<ReservaDTO>> GetPendientes()
        {
            return await _httpJsonProvider.GetAsync(Constants.RESERVA_PENDIENTES);
        }

        public async Task<ReservaDTO> GetReservaById(int id)
        {
            return await _httpJsonProvider.GetByIdAsync(Constants.RESERVA_GET_BY_ID, id);
        }


        public async Task PostReserva(ReservaDTO pedido)
        {
            try
            {
                if (pedido == null) return;
                var response = await _httpJsonProvider.PostAsync(Constants.RESERVA_CREAR, pedido);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear reserva: {ex.Message}");
            }
        }

       // public async Task PutReserva(ReservaDTO pedido)
       // {
         //   try
          //  {
           //     if (pedido == null) return;
           //     string url = string.Format(Constants.RESERVA_PATCH, pedido.Id);

            //    var response = await _httpJsonProvider.PutAsync(url, pedido);
           // }
          //  catch (Exception ex)
          // {
            //    Console.WriteLine($"Error al modificar reserva: {ex.Message}");
           // }
      //  }
      

        public async Task PutReserva(ReservaDTO pedido)
        {
            try
            {
                if (pedido == null) return;
                var response = await _httpJsonProvider.PutAsync(Constants.RESERVA_GET_ALL + "/" + pedido.Id, pedido);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public async Task CambiarEstado(ReservaDTO reserva)
        {
            try
            {
                if (reserva == null) return;
                await _httpJsonProvider.PutAsync(Constants.RESERVA_CAMBIAR_ESTADO, reserva);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar estado: {ex.Message}");
            }
        }

        public async Task DeleteReserva(int id)
        {
            try
            {
                string url = string.Format(Constants.RESERVA_GET_BY_ID, id);
                await _httpJsonProvider.Delete(url, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar reserva: {ex.Message}");
            }
        }
    }
}

