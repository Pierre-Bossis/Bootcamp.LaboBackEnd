using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.Domain;
using Bootcamp.LaboBackEnd.DTOs.Commande;
using Bootcamp.LaboBackEnd.DTOs.Mappers;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Policy = "adminPolicy")]
        [HttpGet]
        public IActionResult GetAllCommandes()
        {
            IEnumerable<GetCommandeDTO> commandes =  _commandeService.GetAllCommandes().Select(c => c.ToCommandeDTO());


            return Ok(commandes);
        }

        [Authorize(Policy = "connectedPolicy")]
        [HttpPost("create-commande")]
        public IActionResult CreateCommande(IEnumerable<Commande_Produit> commande_produit)
        {
            Guid? userId = GetCurrentUserId();
            if (userId is null) return Unauthorized("Utilisateur non authentifié.");

            bool success = _commandeService.CreateCommande(userId.Value, commande_produit);

            if(!success) return BadRequest("Erreur lors de la création de la commande.");

            return Ok("Commande passée.");
        }

        [Authorize(Policy = "adminPolicy")]
        [HttpPut("update-state/{id}")]
        public IActionResult updateState([FromRoute]int id, [FromQuery]int stateId)
        {
            GetSummaryCommandeDto dto = _commandeService.UpdateStateCommande(id, stateId).ToSummaryCommandeDTO();

            if(dto.Id == 0) return NotFound("Commande non trouvée.");

            return Ok(dto);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetCommandeById([FromRoute]int id)
        {
            GetCommandeDTO dto = _commandeService.GetCommandeById(id).ToCommandeDTO();

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
