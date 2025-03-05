namespace Bootcamp.LaboBackEnd.DTOs.Utilisateur;

public class ConnectedUtilisateurDTO
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; }
}
