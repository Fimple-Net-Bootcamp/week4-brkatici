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
    public class SocialInteractionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SocialInteractionsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("[controller]/GetSpecificPetInterAction{petId}")]
        public ActionResult<Pet> GetInteractionById(int petId)
        {
            var interaction = _context.SocialInteractions.Where(i => i.Pet1 == petId || i.Pet2 == petId).ToList();
            if (interaction == null)
            {
                return NotFound(); // Belirtilen petId'ye sahip evcil hayvan bulunamadı
            }
            return Ok(interaction);
        }

        [HttpPost]
        public ActionResult StartSocialInteraction(int pet1Id, int pet2Id)
        {
            SocialInteraction socialInteraction = new SocialInteraction();
            var pet1 = _context.Pets.FirstOrDefault(p => p.PetId == pet1Id);
            var pet2 = _context.Pets.FirstOrDefault(p => p.PetId == pet2Id);
            // Add a new social interaction involving specified pets
            if (pet1Id == null || pet2Id==null)
            {
                return BadRequest("Evcil hayvan bilgileri eksik.");
            }
            else if (pet1==null ||pet2==null)
            {
                return BadRequest("Evcil hayvan bilgileri sorgulandı ve bulunamadı");
            }
            socialInteraction.StartTime = DateTime.Now; 
            socialInteraction.Pet1 = pet1Id;
            socialInteraction.Pet2 = pet2Id;
            _context.SocialInteractions.Add(socialInteraction);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetInteractionById), new { petId = socialInteraction.Pet1 }, socialInteraction);
        }

        //[HttpGet("{evcilHayvanId}")]
        //public ActionResult<IEnumerable<SocialInteraction>> GetPetSocialInteractions(int evcilHayvanId)
        //{
        //    var interactions = _socialInteractionRepository.GetInteractionsByPetId(evcilHayvanId);

        //    if (interactions == null || !interactions.Any())
        //    {
        //        return NotFound("No social interactions found for this pet");
        //    }

        //    return Ok(interactions);
        //}
    }
}
