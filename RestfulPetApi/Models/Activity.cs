using System.Text.Json.Serialization;

namespace RestfulPetApi.Models
{
    public class Activity
    {
        [JsonIgnore]
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public int PetId { get; set; }  
        // Diğer aktivite bilgileri eklenebilir
    }

}
