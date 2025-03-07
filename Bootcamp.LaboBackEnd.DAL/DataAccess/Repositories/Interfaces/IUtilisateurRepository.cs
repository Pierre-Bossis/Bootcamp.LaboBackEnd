using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;

public interface IUtilisateurRepository
{
    /// <summary>
    /// Créer un utilisateur.
    /// </summary>
    /// <param name="utilisateur"></param>
    void Register(Utilisateur utilisateur);
    /// <summary>
    /// Se connecter à un compte utilisateur.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Utilisateur Login(string email);
    /// <summary>
    /// Récupérer le hash du mot de passe d'un utilisateur par son email.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    string GetPasswordHashByEmail(string email);
    /// <summary>
    /// Récupérer l'historique des commandes d'un utilisateur par son id.
    /// </summary>
    /// <param name="utilisateurId"></param>
    /// <returns></returns>
    IEnumerable<Commande> HistoriqueCommandesByUtilisteurId(Guid utilisateurId);
}
