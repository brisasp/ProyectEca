using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DesignAPI.Models.Entity
{
    public class FranjaHorarioEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public TimeSpan HoraInicio { get; set; }

        [MaxLength(100)]
        public TimeSpan HoraFin { get; set; }
        public bool Activa { get; set; }
    }
}