using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DesignAPI.Models.DTOs.FranjaHorarioDTO
{
    public class CreateFranjaHorarioDTO
    {
        [Required(ErrorMessage = "HoraInicio is required")]
     
        public string HoraInicio { get; set; }
     
        [Required(ErrorMessage = "HoraFin is required")]
      
        public string HoraFin { get; set; }


        [Required(ErrorMessage = "Activa is required")]
        public bool Activa { get; set; }
    }
}
