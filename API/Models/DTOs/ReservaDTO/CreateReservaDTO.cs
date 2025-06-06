﻿using System.ComponentModel.DataAnnotations;

namespace DesignAPI.Models.DTOs.ReservaDTO
{
    public class CreateReservaDTO
    {
        [Required(ErrorMessage = "Fecha is required")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "HoraInicio is required")]
        public string HoraInicio { get; set; }

        [Required(ErrorMessage = "HoraFin is required")]
        public string HoraFin { get; set; }

        [Required(ErrorMessage = "Grupo is required")]
        [MaxLength(50, ErrorMessage = "Max char is 50")]
        public string Grupo { get; set; }
        [Required(ErrorMessage = "NombreProfesor is required")]
        public string NombreProfesor { get; set; }
        public string CorreoProfesor { get; set; }
        [Required(ErrorMessage = "Estado is required")]
        public string Estado { get; set; }
    }
}
