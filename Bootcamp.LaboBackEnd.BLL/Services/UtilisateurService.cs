using Bootcamp.LaboBackEnd.BLL.CustomExceptions;
using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;
using Bootcamp.LaboBackEnd.Domain;
using Isopoh.Cryptography.Argon2;

namespace Bootcamp.LaboBackEnd.BLL.Services;

public class UtilisateurService : IUtilisateurService
{
    private readonly IUtilisateurRepository _utilisateurRepository;

    public UtilisateurService(IUtilisateurRepository utilisateurRepository)
    {
        _utilisateurRepository = utilisateurRepository;
    }

    public void Register(Utilisateur utilisateur)
    {
        bool emailExists = _utilisateurRepository.IsEmailAlreadyExists(utilisateur.Email);
        if (emailExists) throw new EmailAlreadyExistsException();

        utilisateur.PasswordHash = Argon2.Hash(utilisateur.PasswordHash);

        bool success = _utilisateurRepository.Register(utilisateur);
        if (!success) throw new BusinessException("Erreur inattendue lors de la création.");
    }

    public Utilisateur? Login(string email, string password)
    {
        string? storedPasswordHash = _utilisateurRepository.GetPasswordHashByEmail(email);

        if (storedPasswordHash is null) return null;

        bool verifyPassword = Argon2.Verify(storedPasswordHash, password);

        if (verifyPassword) return _utilisateurRepository.Login(email);

        return null;
    }

    public IEnumerable<Commande> historiqueCommandesByUtilisteurId(Guid utilisateurId)
    {
        try
        {
            return _utilisateurRepository.HistoriqueCommandesByUtilisteurId(utilisateurId);
        }
        catch (Exception ex)
        {
            throw new BusinessException(ex.Message, ex);
        }
    }

    public Utilisateur Update(Guid id, Utilisateur utilisateur)
    {
        try
        {
        return _utilisateurRepository.Update(id, utilisateur);
        }
        catch (Exception ex)
        {
            throw new BusinessException(ex.Message, ex);
        }
    }
}