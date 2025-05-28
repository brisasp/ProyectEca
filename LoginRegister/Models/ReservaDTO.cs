using System.Text.Json.Serialization;


namespace LoginRegister.Models
{
    public class ReservaDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }


        [JsonPropertyName("nombreProfesor")]
        public string NombreProfesor { get; set; }

        [JsonPropertyName("fecha")]
        public DateTime Fecha { get; set; }

        [JsonPropertyName("horaInicio")]
        public string HoraInicio { get; set; }

        [JsonPropertyName("horaFin")]
        public string HoraFin { get; set; }

        [JsonPropertyName("grupo")]
        public string Grupo { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get;set; }
    }
}

  
  





