using Bootcamp.LaboBackEnd.DTOs.Categorie;

namespace Bootcamp.LaboBackEnd.DTOs.Mappers;

public static class CategorieMapper
{
    public static CategorieDTO ToDTO(this Domain.Categorie categorie)
    {
        if (categorie is null) return new CategorieDTO();

        return new CategorieDTO()
        {
            Id = categorie.Id,
            Nom = categorie.Nom
        };
    }
}
