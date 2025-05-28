using System.ComponentModel.DataAnnotations;

namespace DesignAPI.Models.DTOs.ProfesorDTO
{
    public class CreateDiaNoLectivoDTO
    {
        [Required(ErrorMessage = "Fecha is required")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Motivo is required")]
        [MaxLength(50, ErrorMessage = "Max char is 50")]
        public string Motivo { get; set; }
    }
}
