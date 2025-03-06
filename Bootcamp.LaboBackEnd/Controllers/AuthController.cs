﻿using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.DTOs.Mappers;
using Bootcamp.LaboBackEnd.DTOs.Utilisateur;
using Bootcamp.LaboBackEnd.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        //[HttpGet("getjwt")]
        //public IActionResult GetJwt()
        //{
        //    string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var userId = User.FindFirst(ClaimTypes.Sid)?.Value;
        //        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        //    }
        //    else
        //    {
        //        return Unauthorized("Vous devez être authentifié.");
        //    }
        //    return Ok(token);
        //}

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterFormUtilisateurDTO utilisateur)
        {
            if (!ModelState.IsValid) return BadRequest("Elements non valides.");

            _utilisateurService.Register(utilisateur.ToEntity());
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginFormUtilisateurDTO form)
        {
            ConnectedUtilisateurDTO utilisateur = _utilisateurService.Login(form.Email, form.Password).ToDTO();

            if (utilisateur.Id == Guid.Empty) return BadRequest("Email ou Mot de passe invalide.");

            string token = _jwtGenerator.Generate(utilisateur);

            return Ok(token);
        }
    }
}
