using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulPetApi.Data;
using RestfulPetApi.Models;

namespace RestfulPetApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PetsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[controller]/GetAllPets")]
        public IActionResult GetEvcilHayvanlar()
        {
            List<Pet> pets = _context.Pets.ToList(); // Tüm evcil hayvanları al

            return Ok(pets);
        }

        [HttpGet]
        [Route("[controller]/GetSpecificPet{petId}")]
        public ActionResult<Pet> GetPetById(int petId)
        {
            var pet = _context.Pets.FirstOrDefault(p => p.PetId == petId);
            if (pet == null)
            {
                return NotFound(); // Belirtilen petId'ye sahip evcil hayvan bulunamadı
            }
            return Ok(pet);
        }

        [HttpPost]
        [Route("[controller]/CreatePet")]
        public IActionResult CreatePet([FromBody] Pet pet)
        {
            if (pet == null)
            {
                return BadRequest("Evcil hayvan bilgileri eksik.");
            }

            _context.Pets.Add(pet);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPetById), new { petId = pet.PetId }, pet);
        }
        [HttpPut]
        [Route("[controller]/UpdatePet{petId}")]
        public IActionResult UpdatePet(int petId, [FromBody] Pet updatedPet)
        {
            if (updatedPet == null)
            {
                return BadRequest("Evcil hayvan bilgileri eksik veya evcilHayvanId uyumsuz.");
            }

            var existingPet = _context.Pets.Find(petId);
            if (existingPet == null)
            {
                return NotFound("Belirtilen evcil hayvan bulunamadı.");
            }

            existingPet.Name = updatedPet.Name;
            existingPet.Species = updatedPet.Species;
            existingPet.Age = updatedPet.Age;   
            existingPet.Gender = updatedPet.Gender; 
            existingPet.UserId = updatedPet.UserId;

            // Diğer özelliklerin güncellenmesi

            _context.Pets.Update(existingPet);
            _context.SaveChanges();

            return Ok(); // 204 No Content döner
        }


        [HttpGet("[controller]/Statistics{petId}\"")]
        public ActionResult<PetStatistics> GetPetStatistics(int petId)
        {
            var pet = _context.Pets.FirstOrDefault(p => p.PetId == petId);
            var petActivity = _context.Activities.Where(p => p.PetId == petId).ToList();
            var petHealth = _context.HealthStatuses.Where(p => p.PetId == petId).ToList();
            var petFoods = _context.Foods.Where(p => p.PetId == petId).ToList();
            if (pet == null)
            {
                return NotFound(); // Evcil hayvan bulunamazsa 404 döndür
            }

            // Burada istatistikleri hazırlayarak döndürebilirsiniz
            var petStatistics = new PetStatistics
            {
                PetId = pet.PetId,
                Activities = petActivity,
                HealthStatus = petHealth,
                Foods = petFoods
                // İlgili diğer istatistik özelliklerini doldurun
            };

            return Ok(petStatistics); // İstatistikleri 200 OK koduyla döndür
        }



    }
}
