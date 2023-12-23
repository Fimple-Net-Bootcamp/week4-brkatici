using System.Text.Json.Serialization;

namespace RestfulPetApi.Models
{
    public class HealthStatus
    {
        [JsonIgnore]
        public int HealthStatusId { get; set; }
        [JsonIgnore]
        public int PetId { get; set; } // Evcil hayvanın ID'si
        [JsonIgnore]
        public Pet Pet { get; set; } // Evcil hayvana referans
        public double Weight { get; set; }
        public DateTime LastCheckupDate { get; set; }
        public string VaccinationRecords { get; set; }
        public string Diseases { get; set; }
        // Diğer sağlık bilgileri eklenebilir
    }

}
