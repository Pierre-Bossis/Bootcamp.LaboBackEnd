using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;

public interface IProduitRepository
{
    /// <summary>
    /// Réccupérer tous les produits.
    /// </summary>
    /// <returns></returns>
    IEnumerable<Produit> GetAllProduits();
    /// <summary>
    /// Réccupérer un produit par son id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Produit? GetProduitById(int id);
    /// <summary>
    /// Ajouter un produit.
    /// </summary>
    /// <param name="produit"></param>
    /// <returns></returns>
    Produit AddProduit(Produit produit);
    /// <summary>
    /// Mettre à jour un produit.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produit"></param>
    /// <returns></returns>
    Produit UpdateProduit(int id, Produit produit);
    /// <summary>
    /// Supprimer un produit.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool DeleteProduit(int id);
    /// <summary>
    /// Filtre les produits par id de catégorie.
    /// </summary>
    /// <param name="categorieId"></param>
    /// <returns></returns>
    IEnumerable<Produit> GetProduitsByCategorieId(int categorieId);
    /// <summary>
    /// Filtre les produits par nom de catégorie.
    /// </summary>
    /// <param name="nom"></param>
    /// <returns></returns>
    IEnumerable<Produit> GetProduitsByCategorieName(string nom);
}
