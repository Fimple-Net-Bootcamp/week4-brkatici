using System.Text.Json.Serialization;

namespace RestfulPetApi.Models
{
    public class User
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegistrationDate { get; set; }

        public List<Pet> Pets { get; set; }
        // Other fields can be added
    }

}
