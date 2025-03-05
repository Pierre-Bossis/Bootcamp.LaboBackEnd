using Bootcamp.LaboBackEnd.DTOs.Utilisateur;

namespace Bootcamp.LaboBackEnd.DTOs.Mappers;

public static class UtilisateurMapper
{
    public static Domain.Utilisateur ToEntity(this RegisterFormUtilisateurDTO dto)
    {
        if (dto is null) return null;

        return new Domain.Utilisateur()
        {
            Nom = dto.Nom,
            Prenom = dto.Prenom,
            Email = dto.Email,
            PasswordHash = dto.Password
        };
    }

    public static ConnectedUtilisateurDTO ToDTO(this Domain.Utilisateur utilisateur)
    {
        if (utilisateur is null) return null;

        return new ConnectedUtilisateurDTO()
        {
            Id = utilisateur.Id,
            Email = utilisateur.Email,
            IsAdmin = utilisateur.IsAdmin
        };
    }
}
