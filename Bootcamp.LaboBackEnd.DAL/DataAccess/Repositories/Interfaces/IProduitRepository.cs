using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;

public interface IProduitRepository
{
    IEnumerable<Produit> GetAllProduits();
    Produit? GetProduitById(int id);
    Produit AddProduit(Produit produit);
    Produit UpdateProduit(int id, Produit produit);
    bool DeleteProduit(int id);
}
