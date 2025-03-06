using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.Domain;
using Bootcamp.LaboBackEnd.DTOs.Categorie;
using Bootcamp.LaboBackEnd.DTOs.Mappers;
using Bootcamp.LaboBackEnd.DTOs.Produit;
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

        [HttpPost("create-product")]
        public IActionResult CreateProduct([FromBody] CreateFormProduitDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest("Elements non valides.");

            //récupérer la categorie pour retourner dans l'objet final
            CategorieDTO? categorieDTO = _categorieService.GetCategorieById(dto.CategorieId).ToDTO();
            if (categorieDTO is null) return BadRequest("La catégorie n'existe pas.");

            //créé le produit et retourne l'entitée créée avec les informations complètes de la catégorie
            ProduitDTO createdProduct = _produitService.AddProduit(dto.ToEntity()).ToDtoFull(categorieDTO);
            if (createdProduct is null) return BadRequest("Erreur lors de la création du produit.");

            return Ok(createdProduct);
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            //charger toutes les catégories pour les mapper dans les dto
            Dictionary<int, CategorieDTO> categories = _categorieService.GetAllCategories()
                    .ToDictionary(c => c.Id, c => c.ToDTO());

            //récupérer tous les produits et les mapper avec les catégories
            IEnumerable<ProduitDTO> produits = _produitService.GetAllProduits()
                                .Select(p => p.ToDtoFull(categories[p.CategorieId]));

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
            ProduitDTO produitDTO = produit.ToDtoFull(categorieDTO);

            return Ok(produitDTO);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            bool success = _produitService.DeleteProduit(id);

            if (!success) return BadRequest("Erreur lors de la suppression");

            return Ok("Produit supprimé avec succès");
        }

        [HttpPut("update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody]  Produit produit)
        {
            if (!ModelState.IsValid) return BadRequest("Elements non valides.");

            return Ok();
        }
    }
}
