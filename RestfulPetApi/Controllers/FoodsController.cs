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
    public class FoodsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public FoodsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[controller]/GetAllFoods")]
        public IActionResult GetEvcilHayvanlar()
        {
            List<Food> foods = _context.Foods.ToList(); // Tüm evcil hayvanları al

            return Ok(foods);
        }


        [HttpPost]
        [Route("[controller]/FeedThePet/{petId}&{foodId}")]
        public IActionResult FeedPet(int petId, int foodId)
        {
            var food = _context.Foods.FirstOrDefault(f => f.FoodId == foodId);

            if (food == null)
            {
                return NotFound("Belirtilen besin bulunamadı.");
            }

            // İlgili besinin petId'sini güncelleme
            food.PetId = petId;
            _context.SaveChanges();

            return Ok($"Evcil hayvan ({petId}) için besin ({foodId}) başarıyla verildi.");
        }


    }
}
