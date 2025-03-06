using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;

public interface ICommandeRepository
{
    bool CreateCommande(Guid utilisateurId, IEnumerable<Commande_Produit> produits);
    IEnumerable<Commande> GetAllCommandes();
    Commande UpdateStateCommande(int id, int stateId);
}
