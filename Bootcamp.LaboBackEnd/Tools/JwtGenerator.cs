using Bootcamp.LaboBackEnd.DTOs.Utilisateur;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bootcamp.LaboBackEnd.Tools;

public class JwtGenerator
{
    private IConfiguration _config;

    public JwtGenerator(IConfiguration config)
    {
        _config = config;
    }
    public string Generate(ConnectedUtilisateurDTO user)
    {
        if (user is null)
        {
            throw new ArgumentNullException("user");
        }

        string key = _config.GetSection("tokenInfo").GetSection("secretKey").Value;
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        SigningCredentials signingKey = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        Claim[] myClaims =
        {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
                new Claim(ClaimTypes.Email, user.Email)
        };

        JwtSecurityToken jwt = new JwtSecurityToken(
                claims: myClaims,
                signingCredentials: signingKey,
                expires: DateTime.Now.AddDays(1),
                issuer: "https://localhost:7155",   // Ajout de l'issuer
                audience: "http://localhost:4200"
            );

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        return handler.WriteToken(jwt);
    }
}
