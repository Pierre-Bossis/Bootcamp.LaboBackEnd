using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.BLL.Services.Interfaces;

public interface ICommandeService
{
    Commande CreateCommande(Commande commande, Guid utilisateurId);
    IEnumerable<Commande> GetAllCommandes();
    Commande UpdateStateCommande(int id, int stateId);
}
