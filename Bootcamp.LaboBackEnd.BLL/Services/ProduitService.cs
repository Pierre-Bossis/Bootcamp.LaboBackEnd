using Bootcamp.LaboBackEnd.BLL.CustomExceptions;
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
        try
        {
            bool produitNameExists = _Repository.IsProduitNameExists(produit.Nom);
            if (produitNameExists) throw new NameCategorieAlreadyExistsException();

            return _Repository.AddProduit(produit);
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Une Exception inconnue est survenue: {ex.Message}", ex);
        }
    }

    public bool DeleteProduit(int id)
    {
        try
        {
            return _Repository.DeleteProduit(id);
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Une Exception inconnue est survenue: {ex.Message}", ex);
        }
    }

    public IEnumerable<Produit> GetAllProduits()
    {
        try
        {
            return _Repository.GetAllProduits();
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Une Exception inconnue est survenue: {ex.Message}", ex);
        }
    }

    public Produit? GetProduitById(int id)
    {
        try
        {
            return _Repository.GetProduitById(id);
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Une Exception inconnue est survenue: {ex.Message}", ex);
        }
    }

    public IEnumerable<Produit> GetProduitsByCategorieId(int categorieId)
    {
        try
        {
            return _Repository.GetProduitsByCategorieId(categorieId);
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Une Exception inconnue est survenue: {ex.Message}", ex);
        }
    }
    public IEnumerable<Produit> GetProduitsByCategorieName(string nom)
    {
        try
        {
            return _Repository.GetProduitsByCategorieName(nom);
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Une Exception inconnue est survenue: {ex.Message}", ex);
        }
    }

    public Produit UpdateProduit(int id, Produit produit)
    {
        try
        {
            return _Repository.UpdateProduit(id, produit);
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Une Exception inconnue est survenue: {ex.Message}", ex);
        }
    }
}
