using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.Domain;
using Bootcamp.LaboBackEnd.DTOs.Categorie;
using Bootcamp.LaboBackEnd.DTOs.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bootcamp.LaboBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        private readonly ICategorieService _Service;

        public CategorieController(ICategorieService service)
        {
            _Service = service;
        }

        [HttpGet("getjwt")]
        public IActionResult GetJwt()
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            }
            else
            {
                return Unauthorized("Vous devez être authentifié.");
            }
            return Ok(token);
        }

        [Authorize(Policy = "adminPolicy")]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<CategorieDTO> categories = _Service.GetAllCategories().Select(c => c.ToDTO());

            return Ok(categories);
        }

        [HttpGet("id/{id}")]
        public IActionResult GetById(int id)
        {
            CategorieDTO? categorie = _Service.GetCategorieById(id).ToDTO();

            if (categorie == null) return NotFound();

            return Ok(categorie);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            CategorieDTO categorie = _Service.GetCategorieByName(name).ToDTO();

            if (categorie is null) return NotFound();

            return Ok(categorie);
        }


        [HttpPost]
        public IActionResult CreateCategorie(string nom)
        {
            CategorieDTO cat = _Service.AddCategorie(nom).ToDTO();

            return Ok(cat);
        }
    }
}
