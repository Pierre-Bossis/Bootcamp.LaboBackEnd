using System.ComponentModel;

namespace Bootcamp.LaboBackEnd.Domain;

public class Produit
{
	public int Id { get; set; }
    public string Nom { get; set; }
    public decimal Prix { get; set; }
    public int Quantite { get; set; }
    public string Description { get; set; }
    public int CategorieId { get; set; }
    public Categorie Categorie { get; set; }
}
