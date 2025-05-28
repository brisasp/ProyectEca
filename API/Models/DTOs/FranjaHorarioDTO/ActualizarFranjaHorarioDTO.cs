using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DesignAPI.Models.DTOs.FranjaHorarioDTO
{
    public class ActualizarFranjaHorarioDTO
    {
        public int Id { get; set; }
      
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public bool Activa { get; set; }
    }
}
