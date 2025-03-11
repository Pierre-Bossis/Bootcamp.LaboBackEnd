using Bootcamp.LaboBackEnd.BLL.CustomExceptions;
using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;
using Bootcamp.LaboBackEnd.Domain;
using Microsoft.Data.SqlClient;

namespace Bootcamp.LaboBackEnd.BLL.Services;

public class ProduitService : IProduitService
{
    private readonly IProduitRepository _Repository;

    public ProduitService(IProduitRepository produitRepository)
    {
        _Repository = produitRepository;
    }

    public Produit? AddProduit(Produit produit)
    {
        try
        {
            bool produitNameExists = _Repository.IsProduitNameExists(produit.Nom);
            if (produitNameExists) return null;

            return _Repository.AddProduit(produit);
        }
        catch (SqlException ex)
        {
            throw new DatabaseException($"Une erreure SQL est survenue: {ex.Message}",ex);
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Une Exception inconnue est survenue: {ex.Message}",ex);
        }
    }

    public bool DeleteProduit(int id)
    {
        return _Repository.DeleteProduit(id);
    }

    public IEnumerable<Produit> GetAllProduits()
    {
        throw new Exception();
        return _Repository.GetAllProduits();
    }

    public Produit? GetProduitById(int id)
    {
        return _Repository.GetProduitById(id);
    }

    public IEnumerable<Produit> GetProduitsByCategorieId(int categorieId)
    {
        return _Repository.GetProduitsByCategorieId(categorieId);
    }
    public IEnumerable<Produit> GetProduitsByCategorieName(string nom)
    {
        return _Repository.GetProduitsByCategorieName(nom);
    }

    public Produit UpdateProduit(int id, Produit produit)
    {
        return _Repository.UpdateProduit(id, produit);
    }
}
