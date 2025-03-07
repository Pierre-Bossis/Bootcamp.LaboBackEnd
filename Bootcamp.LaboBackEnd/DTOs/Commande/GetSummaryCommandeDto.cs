namespace Bootcamp.LaboBackEnd.DTOs.Commande;

public class GetSummaryCommandeDto
{
    public int Id { get; set; }
    public int EtatId { get; set; }
    public Guid UtilisateurId { get; set; }
    public DateTime Date { get; set; }
}
