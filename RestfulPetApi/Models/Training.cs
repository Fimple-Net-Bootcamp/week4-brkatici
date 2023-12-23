using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RestfulPetApi.Models
{
    public class Training
    {
        [JsonIgnore]
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public int PetId { get; set; }
    }

}
