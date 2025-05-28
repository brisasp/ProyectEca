using System.Text.Json.Serialization;


namespace LoginRegister.Models
{
    public class FranjaHorarioDTO
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("horaInicio")]
        public string HoraInicio { get; set; }

        [JsonPropertyName("horaFin")]
        public string HoraFin { get; set; }

        [JsonPropertyName("activa")]
        public bool Activa { get; set; }
    
        }
    }

  
  





