using Bootcamp.LaboBackEnd.DTOs.Utilisateur;

namespace Bootcamp.LaboBackEnd.DTOs.Mappers;

public static class UtilisateurMapper
{
    public static Domain.Utilisateur ToEntity(this RegisterFormUtilisateurDTO dto)
    {
        if (dto is null) return new Domain.Utilisateur();

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
        if (utilisateur is null) return new ConnectedUtilisateurDTO();

        return new ConnectedUtilisateurDTO()
        {
            Id = utilisateur.Id,
            Email = utilisateur.Email,
            Nom = utilisateur.Nom,
            Prenom = utilisateur.Prenom,
            IsAdmin = utilisateur.IsAdmin
        };
    }

    public static Domain.Utilisateur ToUpdateEntity(this UpdateFormUtilisateurDTO dto)
    {
        if (dto is null) return new Domain.Utilisateur();

        return new Domain.Utilisateur()
        {
            Id = dto.Id,
            Nom = dto.Nom,
            Prenom = dto.Prenom
        };
    }
}
