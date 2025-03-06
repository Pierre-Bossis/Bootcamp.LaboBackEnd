using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;
using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.BLL.Services;

public class CommandeService : ICommandeService
{
    private readonly ICommandeRepository _repository;

    public CommandeService(ICommandeRepository repository)
    {
        _repository = repository;
    }

    public bool CreateCommande(Guid utilisateurId, IEnumerable<Commande_Produit> produits)
    {
        return _repository.CreateCommande(utilisateurId, produits);
    }

    public IEnumerable<Commande> GetAllCommandes()
    {
        return _repository.GetAllCommandes();
    }

    public Commande UpdateStateCommande(int id, int stateId)
    {
        return _repository.UpdateStateCommande(id, stateId);
    }
}
