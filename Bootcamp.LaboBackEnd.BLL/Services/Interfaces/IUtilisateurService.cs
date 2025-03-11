using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.BLL.Services.Interfaces;

public interface IUtilisateurService
{
    void Register(Utilisateur utilisateur);
    Utilisateur Login(string email, string password);
    IEnumerable<Commande> historiqueCommandesByUtilisteurId(Guid utilisateurId);
    Utilisateur Update(Guid id, Utilisateur utilisateur);
}
