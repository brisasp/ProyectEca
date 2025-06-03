using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DesignAPI.Models.Entity
{
    public class ReservaEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required]
        public string CorreoProfesor { get; set; }
        [Required]
        [MaxLength(50)]
        public string NombreProfesor { get; set; }

        [Required]
        [MaxLength(50)]
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Grupo { get; set; }
        public EstadoReserva Estado { get; set; }
        public DateTime FechaSolicitud { get; set; }

    }
    public enum EstadoReserva
    {
        Pendiente = 0,
        Aprobada = 1,
        Rechazada = 2
    }

}