using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.BLL.Services.Interfaces;

public interface ICommandeService
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
}
