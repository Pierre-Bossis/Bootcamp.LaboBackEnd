using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.BLL.Services.Interfaces;

public interface IUtilisateurService
{
    bool Register(Utilisateur utilisateur);
    Utilisateur Login(string email, string password);
    IEnumerable<Commande> historiqueCommandesByUtilisteurId(Guid utilisateurId);
}
