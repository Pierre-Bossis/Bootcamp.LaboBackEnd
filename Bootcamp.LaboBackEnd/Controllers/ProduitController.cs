using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.Domain;
using Bootcamp.LaboBackEnd.DTOs.Categorie;
using Bootcamp.LaboBackEnd.DTOs.Mappers;
using Bootcamp.LaboBackEnd.DTOs.Produit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp.LaboBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitController : ControllerBase
    {
        private readonly IProduitService _produitService;
        private readonly ICategorieService _categorieService;

        public ProduitController(IProduitService produitService, ICategorieService categorieService)
        {
            _produitService = produitService;
            _categorieService = categorieService;
        }
        [Authorize(Policy = "adminPolicy")]
        [HttpPost("create-product")]
        public IActionResult CreateProduct([FromBody] CreateFormProduitDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest("Elements non valides.");

            //récupérer la categorie pour retourner dans l'objet final
            CategorieDTO? categorieDTO = _categorieService.GetCategorieById(dto.CategorieId).ToDTO();
            if (categorieDTO is null) return BadRequest("La catégorie n'existe pas.");

            //créé le produit et retourne l'entitée créée avec les informations complètes de la catégorie
            Produit produit = _produitService.AddProduit(dto.ToEntity());
            if (produit is null) return Conflict("Le nom du produit est déjà utilisé.");
            ProduitDTO createdProduct = produit.ToDtoFull();

            return Ok(createdProduct);
        }

        [HttpGet("categorie/id/{id}")]
        public IActionResult GetByCategorieId([FromRoute] int id)
        {
            IEnumerable<ListProduitDTO> produits = _produitService.GetProduitsByCategorieId(id).Select(p => p.ToListDTO());
            if (!produits.Any()) return NotFound("Aucun produits trouvé.");

            return Ok(produits);
        }

        [HttpGet("categorie/nom/{nom}")]
        public IActionResult GetByCategorieName([FromRoute] string nom)
        {
            IEnumerable<ListProduitDTO> produits = _produitService.GetProduitsByCategorieName(nom).Select(p => p.ToListDTO());
            if (!produits.Any()) return NotFound("Aucun produits trouvé.");

            return Ok(produits);
        }


        [HttpGet]
        public IActionResult GetAllProducts()
        {
            IEnumerable<ListProduitDTO> produits = _produitService.GetAllProduits().Select(p => p.ToListDTO());

            return Ok(produits);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            Produit? produit = _produitService.GetProduitById(id);
            if (produit is null) return NotFound($"Produit avec l'ID {id} non trouvé.");

            Categorie? categorie = _categorieService.GetCategorieById(produit.CategorieId);
            if (categorie is null) return NotFound($"Catégorie avec l'ID {produit.CategorieId} non trouvée.");

            CategorieDTO categorieDTO = categorie.ToDTO();
            ProduitDTO produitDTO = produit.ToDtoFull();

            return Ok(produitDTO);
        }

        [Authorize(Policy = "adminPolicy")]
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            bool success = _produitService.DeleteProduit(id);

            if (!success) return BadRequest("Erreur lors de la suppression");

            return Ok("Produit supprimé avec succès");
        }

        [Authorize(Policy = "adminPolicy")]
        [HttpPut("update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateFormProduitDTO form)
        {
            if (!ModelState.IsValid || form.Id != id) return BadRequest("Elements non valides.");

            Produit updatedProduit = _produitService.UpdateProduit(id, form.ToEntityUpdate());
            Categorie? categorie = _categorieService.GetCategorieById(updatedProduit.CategorieId);
            if (categorie is null) return BadRequest("Erreur lors de la récupération de la catégorie.");

            ProduitDTO produitDTO = updatedProduit.ToDtoFull();

            return Ok(produitDTO);
        }
    }
}
