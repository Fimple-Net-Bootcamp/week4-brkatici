using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulPetApi.Authentication;
using RestfulPetApi.Data;
using RestfulPetApi.Models;

namespace RestfulPetApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }
        public PetsController petsController { get; set; }  

        [HttpPost]
        [Route("[controller]/AddUser")]
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        [HttpGet]
        [Route("[controller]/GetSpecificUser{userId}")]
        public ActionResult<User> GetUserById(int userId)
        {

            var user = _context.Users.FirstOrDefault(u=>u.UserId == userId);
            if (user == null)
            {
                return NotFound(); // Belirtilen userId'ye sahip user bulunamadı
            }
            return Ok(user);

        }

        [HttpGet]
        [Route("[controller]/GetAllUsers")]
        public List<User> GetAllUsers()
        {
            List<User> users = _context.Users.ToList();       
            return users;
        }


        [HttpGet("[controller]/GetUserPetStatistics{userId}")]
        public ActionResult<UserStatistics> GetUserStatistics(int userId)
        {
            var userStatistics = new UserStatistics();
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            userStatistics.UserId = user.UserId;
            var pets = _context.Pets.Where(p=>p.UserId==userId).ToList();
            if (user == null)
            {
                return NotFound(); // Kullanıcı bulunamazsa 404 döndür
            }

            foreach (var pet in pets)
            {
                var petActivity = _context.Activities.Where(p => p.PetId == pet.PetId).ToList();
                var petHealth = _context.HealthStatuses.Where(p => p.PetId == pet.PetId).ToList();
                var petFoods = _context.Foods.Where(p => p.PetId == pet.PetId).ToList();
                userStatistics.PetStatisticsList = user.Pets.Select(pets => new PetStatistics
                {
                    PetId = pet.PetId,
                    Activities = petActivity,
                    HealthStatus = petHealth,
                    Foods = petFoods
                    // İlgili diğer istatistik özelliklerini doldurun
                }).ToList();
                    // Kullanıcıya ait diğer istatistikleri doldurun
            }
            // Kullanıcı istatistiklerini hazırlayın, örneğin kullanıcının evcil hayvanlarının istatistiklerini toplayabilirsiniz
            

            return Ok(userStatistics.PetStatisticsList); // Kullanıcı istatistiklerini 200 OK koduyla döndür
        }



    }
}
