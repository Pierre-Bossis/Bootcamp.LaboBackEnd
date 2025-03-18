using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;

public interface ICommandeRepository
{
    /// <summary>
    /// Créer une Commande avec une liste de Produits.
    /// </summary>
    /// <param name="utilisateurId"></param>
    /// <param name="produits"></param>
    /// <returns></returns>
    bool CreateCommande(Guid utilisateurId, IEnumerable<Commande_Produit> produits);
    /// <summary>
    /// Récupérer toutes les Commandes avec leurs Produits et leurs Categorie associées.
    /// </summary>
    /// <returns></returns>
    IEnumerable<Commande> GetAllCommandes();
    /// <summary>
    /// Mettre à jour l'état d'une Commande.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="stateId"></param>
    /// <returns></returns>
    Commande UpdateStateCommande(int id, int stateId);
    /// <summary>
    /// Récupérer une commande par son Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Commande? GetCommandeById(int id);
}
