using Bootcamp.LaboBackEnd.DTOs.Produit;

namespace Bootcamp.LaboBackEnd.DTOs.Commande;

public class GetCommandeDTO
{
    public int Id { get; set; }
    public int EtatId { get; set; }
    public Guid UtilisateurId { get; set; }
    public DateTime Date { get; set; }
    public IEnumerable<ListProduitDTO> Produits { get; set; }
}
