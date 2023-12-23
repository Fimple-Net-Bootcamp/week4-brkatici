using System.Text.Json.Serialization;

namespace RestfulPetApi.Models
{
    public class Pet
    {
        [JsonIgnore]
        public int PetId { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public List<Activity> activities = new List<Activity>();

        // Kullanıcıya ait olan ID
        public int? UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; } // Bu alana kullanıcı referansını eklemeyi unutmayın

        // Diğer alanlar eklenebilir
    }

}
