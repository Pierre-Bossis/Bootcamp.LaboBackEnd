using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.DTOs.Mappers;
using Bootcamp.LaboBackEnd.DTOs.Utilisateur;
using Bootcamp.LaboBackEnd.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp.LaboBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;
        private readonly JwtGenerator _jwtGenerator;

        public AuthController(IUtilisateurService utilisateurService, JwtGenerator jwtGenerator)
        {
            _utilisateurService = utilisateurService;
            _jwtGenerator = jwtGenerator;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterFormUtilisateurDTO utilisateur)
        {
            _utilisateurService.Register(utilisateur.ToEntity());
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginFormUtilisateurDTO form)
        {
            ConnectedUtilisateurDTO utilisateur = _utilisateurService.Login(form.Email, form.Password).ToDTO();

            if (utilisateur is null) return BadRequest("Email ou Mot de passe invalide.");

            string token = _jwtGenerator.Generate(utilisateur);

            return Ok(token);
        }
    }
}
