using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulPetApi.Data;
using RestfulPetApi.Models;

namespace RestfulPetApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {

        private readonly AppDbContext _context;
        public TrainingController(AppDbContext context)
        {
            _context = context;
        }
        Training training = new Training();
        public List<Training> PetTrainings = new List<Training>();

        [HttpPost]
        public ActionResult AddTraining(Training training)
        {
            if (training == null)
            {
                return NotFound("Missing training information");
            }
            _context.Trainings.Add(training);
            _context.SaveChanges();
            return Ok("Training added successfully");
        }

        [HttpGet]
        [Route("[controller]/GetSpecificPetTraining{petId}")]
        public ActionResult<Pet> GetPetTraining(int petId)
        {

            
             PetTrainings = _context.Trainings.Where(p => p.PetId == petId).ToList();
            if (PetTrainings == null)
            {
                return NotFound(); // Belirtilen petId'ye sahip evcil hayvan bulunamadı
            }
            return Ok(PetTrainings);
        }
    }
}
