using System.ComponentModel.DataAnnotations;

namespace Bootcamp.LaboBackEnd.DTOs.Utilisateur;

public class LoginFormUtilisateurDTO
{
    [Required, EmailAddress, MinLength(2)]
    public string Email { get; set; }
    [Required, DataType(DataType.Password)]
    public string Password { get; set; }
}
