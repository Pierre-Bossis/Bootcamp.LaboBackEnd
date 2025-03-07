using Bootcamp.LaboBackEnd.DTOs.Categorie;

namespace Bootcamp.LaboBackEnd.DTOs.Produit;

public class ListProduitDTO
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public decimal Prix { get; set; }
    public CategorieDTO categorie { get; set; }
}
