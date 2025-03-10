using System.ComponentModel.DataAnnotations;

namespace Bootcamp.LaboBackEnd.DTOs.Utilisateur;

public class UpdateFormUtilisateurDTO
{
    public Guid Id { get; set; }
    [Required, MinLength(2)]
    public string Nom { get; set; }
    [Required, MinLength(2)]
    public string Prenom { get; set; }
}
