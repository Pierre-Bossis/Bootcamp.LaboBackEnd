using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;

public interface ICommandeRepository
{
    Commande CreateCommande(Commande commande, Guid utilisateurId);
    IEnumerable<Commande> GetAllCommandes();
    Commande UpdateStateCommande(int id, int stateId);
}
