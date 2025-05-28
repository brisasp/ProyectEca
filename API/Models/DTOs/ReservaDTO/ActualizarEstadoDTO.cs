using System.ComponentModel.DataAnnotations;

namespace DesignAPI.Models.DTOs.ReservaDTO
{
        public class UpdateReservaEstadoDTO
        {
            public int ReservaId { get; set; }
            public string Estado { get; set; } // "Aprobada" o "Rechazada"
        }

   }

