namespace Bootcamp.LaboBackEnd.Domain;

public class Commande
{
	public int Id { get; set; }
    public int EtatId { get; set; }
    public Guid UtilisateurId { get; set; }
    public DateTime Date { get; set; }
    public List<Produit> Produits { get; set; } = new();
}
