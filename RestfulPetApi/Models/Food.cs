namespace RestfulPetApi.Models
{
    public class Food
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PetId { get; set; }  
        // Besinle ilgili diğer bilgiler eklenebilir
    }

}
