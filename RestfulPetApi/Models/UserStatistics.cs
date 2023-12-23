namespace RestfulPetApi.Models
{
    public class UserStatistics
    {
        public int UserId { get; set; }
        public List<PetStatistics> PetStatisticsList { get; set; }
        // Diğer kullanıcı istatistik özellikleri...
    }

}
