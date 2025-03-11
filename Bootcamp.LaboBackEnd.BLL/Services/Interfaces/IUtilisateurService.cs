using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.BLL.Services.Interfaces;

public interface IUtilisateurService
{
    /// <summary>
    /// Créer un compte.
    /// </summary>
    /// <param name="utilisateur"></param>
    void Register(Utilisateur utilisateur);
    /// <summary>
    /// Se connecter à un compte.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Utilisateur Login(string email, string password);
    /// <summary>
    /// Obtenir l'historique de commande pour un utilisateur.
    /// </summary>
    /// <param name="utilisateurId"></param>
    /// <returns></returns>
    IEnumerable<Commande> historiqueCommandesByUtilisteurId(Guid utilisateurId);
    /// <summary>
    /// Modifier un utilisateur.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="utilisateur"></param>
    /// <returns></returns>
    Utilisateur Update(Guid id, Utilisateur utilisateur);
}
