using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.Domain;
using Bootcamp.LaboBackEnd.DTOs.Commande;
using Bootcamp.LaboBackEnd.DTOs.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bootcamp.LaboBackEnd.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeController : ControllerBase
    {
        private readonly ICommandeService _commandeService;

        public CommandeController(ICommandeService commandeService)
        {
            _commandeService = commandeService;
        }

        [HttpGet]
        public IActionResult GetAllCommandes()
        {
            IEnumerable<GetCommandeDTO> commandes =  _commandeService.GetAllCommandes().Select(c => c.ToCommandeDTO());


            return Ok(commandes);
        }

        [HttpPost("create-commande")]
        public IActionResult CreateCommande(IEnumerable<Commande_Produit> commande_produit)
        {
            Guid? userId = GetCurrentUserId();
            if (userId is null) return Unauthorized("Utilisateur non authentifié.");

            bool success = _commandeService.CreateCommande(userId.Value, commande_produit);

            if(!success) return BadRequest("Erreur lors de la création de la commande.");

            return Ok("Commande passée.");
        }

        [HttpPut("update-state/{id}")]
        public IActionResult updateState([FromRoute] int id, [FromBody] int stateId)
        {
            GetSummaryCommandeDto dto = _commandeService.UpdateStateCommande(id, stateId).ToSummaryCommandeDTO();

            if(dto.Id == 0) return NotFound("Commande non trouvée.");

            return Ok(dto);
        }

        private Guid? GetCurrentUserId()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;
                if (Guid.TryParse(userIdClaim, out var userId))
                {
                    return userId;
                }
            }
            return null;
        }
    }
}
