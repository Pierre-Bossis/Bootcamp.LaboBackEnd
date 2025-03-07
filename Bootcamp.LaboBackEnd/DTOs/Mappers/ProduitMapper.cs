using Bootcamp.LaboBackEnd.DTOs.Categorie;
using Bootcamp.LaboBackEnd.DTOs.Produit;

namespace Bootcamp.LaboBackEnd.DTOs.Mappers;

public static class ProduitMapper
{
    public static Domain.Produit ToEntity(this CreateFormProduitDTO dto)
    {
        if (dto is null) return new Domain.Produit();

        return new Domain.Produit
        {
            Nom = dto.Nom,
            Description = dto.Description,
            Prix = dto.Prix,
            Quantite = dto.Quantite,
            CategorieId = dto.CategorieId
        };
    }

    public static ProduitDTO ToDtoFull(this Domain.Produit entity)
    {
        if (entity is null) return new ProduitDTO();

        return new ProduitDTO
        {
            Id = entity.Id,
            Nom = entity.Nom,
            Description = entity.Description,
            Prix = entity.Prix,
            Quantite = entity.Quantite,
            Categorie = entity.Categorie.ToDTO()
        };
    }

    public static Domain.Produit ToEntityUpdate(this UpdateFormProduitDTO dto)
    {
        if (dto is null) return new Domain.Produit();

        return new Domain.Produit
        {
            Id = dto.Id,
            Nom = dto.Nom,
            Description = dto.Description,
            Prix = dto.Prix,
            Quantite = dto.Quantite,
            CategorieId = dto.CategorieId
        };
    }

    public static ListProduitDTO ToListDTO(this Domain.Produit entity)
    {
        if (entity is null) return new ListProduitDTO();

        return new ListProduitDTO
        {
            Id = entity.Id,
            Nom = entity.Nom,
            Prix = entity.Prix,
            categorie = entity.Categorie.ToDTO()
        };
    }
}
