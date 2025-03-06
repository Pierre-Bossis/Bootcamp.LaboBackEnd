using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.BLL.Services.Interfaces;

public interface IProduitService
{
    IEnumerable<Produit> GetAllProduits();
    Produit? GetProduitById(int id);
    Produit AddProduit(Produit produit);
    Produit UpdateProduit(int id, Produit produit);
    bool DeleteProduit(int id);
}
