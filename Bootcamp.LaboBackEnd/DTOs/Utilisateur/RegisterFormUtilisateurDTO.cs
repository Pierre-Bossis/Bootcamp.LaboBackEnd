using System.ComponentModel.DataAnnotations;

namespace Bootcamp.LaboBackEnd.DTOs.Utilisateur;

public class RegisterFormUtilisateurDTO
{
    [Required, MinLength(2)]
    public string Nom { get; set; }
    [Required, MinLength(2)]
    public string Prenom { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required, MinLength(8), DataType(DataType.Password)]
    public string Password { get; set; }
}
