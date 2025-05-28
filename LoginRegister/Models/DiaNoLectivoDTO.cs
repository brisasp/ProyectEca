using System.Text.Json.Serialization;


namespace LoginRegister.Models
{
    public class DiaNoLectivoDTO
    {
       [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("fecha")]
        public DateTime Fecha { get; set; }

       [JsonPropertyName("motivo")]
        public string Motivo { get; set; }

    }
}

  
  





