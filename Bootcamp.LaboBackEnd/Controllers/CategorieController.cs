using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.Domain;
using Bootcamp.LaboBackEnd.DTOs.Categorie;
using Bootcamp.LaboBackEnd.DTOs.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            if (categorie.Id == 0) return NotFound();

            return Ok(categorie);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            CategorieDTO categorie = _Service.GetCategorieByName(name).ToDTO();

            if (categorie.Id == 0) return NotFound();

            return Ok(categorie);
        }

        [Authorize(Policy = "adminPolicy")]
        [HttpPost]
        public IActionResult CreateCategorie([FromBody] CreateFormCategorieDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest("Elements non valides.");

            Categorie? categorie = _Service.AddCategorie(dto.Nom);
            if (categorie is null) return Conflict("La catégorie existe déjà.");
            CategorieDTO categorieDTO = categorie.ToDTO();

            return Ok(categorieDTO);
        }
    }
}
