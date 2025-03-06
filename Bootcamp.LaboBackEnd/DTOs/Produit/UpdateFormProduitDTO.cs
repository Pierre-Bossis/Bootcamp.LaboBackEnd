using Bootcamp.LaboBackEnd.DTOs.Categorie;

namespace Bootcamp.LaboBackEnd.DTOs.Produit;

public class UpdateFormProduitDTO
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public decimal Prix { get; set; }
    public int Quantite { get; set; }
    public string Description { get; set; }
    public int CategorieId { get; set; }
}
