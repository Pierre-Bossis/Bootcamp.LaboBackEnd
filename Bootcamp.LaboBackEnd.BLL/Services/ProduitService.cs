using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;
using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.BLL.Services;

public class ProduitService : IProduitService
{
    private readonly IProduitRepository _Repository;

    public ProduitService(IProduitRepository produitRepository)
    {
        _Repository = produitRepository;
    }

    public Produit AddProduit(Produit produit)
    {
        return _Repository.AddProduit(produit);
    }

    public bool DeleteProduit(int id)
    {
        return _Repository.DeleteProduit(id);
    }

    public IEnumerable<Produit> GetAllProduits()
    {
        return _Repository.GetAllProduits();
    }

    public Produit? GetProduitById(int id)
    {
        return _Repository.GetProduitById(id);
    }

    public Produit UpdateProduit(int id, Produit produit)
    {
        return _Repository.UpdateProduit(id, produit);
    }
}
