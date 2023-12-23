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
    public class ActivitiesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ActivitiesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[controller]/GetActivities")]
        public IActionResult GetActivities()
        {
            List<Activity> activities = _context.Activities.ToList(); // Tüm evcil hayvanları al

            return Ok(activities);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Route("[controller]/GetSpecificPet{activityId}")]
        public ActionResult<Activity> GetActivityById(int activityId)
        {
            var activity = _context.Activities.FirstOrDefault(a => a.ActivityId == activityId);
            if (activity == null)
            {
                return NotFound(); // Belirtilen petId'ye sahip evcil hayvan bulunamadı
            }
            return Ok(activity);
        }

        [HttpGet]
        [Route("[controller]/GetActivities/{petId}")]
        public IActionResult GetActivities(int petId)
        {
            // Burada _context üzerinden belirli bir petId'ye sahip aktiviteleri getirme işlemi yapılmalıdır
            List<Activity> activities = _context.Activities.Where(a => a.PetId == petId).ToList();

            if (activities == null || !activities.Any())
            {
                return NotFound(); // Belirtilen petId'ye sahip aktivite bulunamadı
            }

            return Ok(activities);
        }

        [HttpPost]
        [Route("[controller]/CreateActivity")]
        public IActionResult CreateActivity([FromBody] Activity newActivity)
        {
            if (newActivity == null)
            {
                return BadRequest("Aktivite bilgileri eksik.");
            }

            // Yeni aktiviteyi veritabanına ekleme işlemi
            _context.Activities.Add(newActivity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetActivityById), new { activityId = newActivity.ActivityId }, newActivity);
        }


    }
}
