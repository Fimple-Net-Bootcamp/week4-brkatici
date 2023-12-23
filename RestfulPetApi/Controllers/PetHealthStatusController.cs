using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulPetApi.Data;
using RestfulPetApi.Models;

namespace RestfulPetApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api")]
    [ApiController]
    public class PetHealthStatusController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PetHealthStatusController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("[controller]/PetHealthStatus/{petId}")]
        public ActionResult<User> GetUserById(int petId)
        {

            var healthStatus = _context.HealthStatuses.FirstOrDefault(u => u.PetId == petId);
            if (healthStatus == null)
            {
                return NotFound(); // Belirtilen userId'ye sahip user bulunamadı
            }
            return Ok(healthStatus);

        }

        [HttpPatch("[controller]/PatchPetHealthData/{petId}")]
        public IActionResult PartiallyUpdateWeatherData(int petId, [FromBody] JsonPatchDocument<HealthStatus> patchDoc)
        {
            var existingData = _context.HealthStatuses.FirstOrDefault(pet => pet.PetId == petId);

            if (existingData == null)
            {
                return NotFound("Belirtilen petId ile eslesen veri bulunamadı.");
            }

            if (patchDoc != null)
            {
                patchDoc.ApplyTo(existingData, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.HealthStatuses.Update(existingData);
                _context.SaveChanges();
                return new ObjectResult(existingData);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


    }
}
