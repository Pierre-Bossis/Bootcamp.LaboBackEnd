using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.BLL.Services.Interfaces;

public interface ICommandeService
{
    bool CreateCommande(Guid utilisateurId, IEnumerable<Commande_Produit> produits);
    IEnumerable<Commande> GetAllCommandes();
    Commande UpdateStateCommande(int id, int stateId);
}
