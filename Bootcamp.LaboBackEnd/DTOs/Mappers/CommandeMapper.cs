using Bootcamp.LaboBackEnd.DTOs.Commande;

namespace Bootcamp.LaboBackEnd.DTOs.Mappers;

public static class CommandeMapper
{
    public static GetCommandeDTO ToCommandeDTO(this Domain.Commande commande)
    {
        return new GetCommandeDTO
        {
            Id = commande.Id,
            Date = commande.Date,
            EtatId = commande.EtatId,
            UtilisateurId = commande.UtilisateurId,
            Produits = commande.Produits.Select(p => p.ToListDTO())
        };
    }
}
