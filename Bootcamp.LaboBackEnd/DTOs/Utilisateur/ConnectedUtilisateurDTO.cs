namespace Bootcamp.LaboBackEnd.DTOs.Utilisateur;

public class ConnectedUtilisateurDTO
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Nom {  get; set; }
    public string Prenom { get; set; }
    public bool IsAdmin { get; set; }
}
