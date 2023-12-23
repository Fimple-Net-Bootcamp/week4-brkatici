using System.Text.Json.Serialization;

namespace RestfulPetApi.Models
{
    public class SocialInteraction
    {
        [JsonIgnore]
        public int SocialInteractionId { get; set; }
        [JsonIgnore]
        public DateTime StartTime { get; set; }
        public int Pet1 { get; set; }
        public int Pet2 { get; set;}
    }

}
