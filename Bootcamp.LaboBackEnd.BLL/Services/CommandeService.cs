using Bootcamp.LaboBackEnd.BLL.CustomExceptions;
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
        try
        {
            return _repository.CreateCommande(utilisateurId, produits);
        }
        catch (Exception ex)
        {
            throw new BusinessException(ex.Message, ex);
        }
    }

    public IEnumerable<Commande> GetAllCommandes()
    {
        try
        {
            return _repository.GetAllCommandes();
        }
        catch (Exception ex)
        {
            throw new BusinessException(ex.Message, ex);
        }
    }

    public Commande? GetCommandeById(int id)
    {
        Commande? commande = _repository.GetCommandeById(id);
        if (commande is null) throw new NotFoundCommandeException();

        return commande;
    }

    public Commande UpdateStateCommande(int id, int stateId)
    {
        try
        {
            return _repository.UpdateStateCommande(id, stateId);
        }
        catch (Exception ex)
        {
            throw new BusinessException(ex.Message, ex);
        }
    }
}
