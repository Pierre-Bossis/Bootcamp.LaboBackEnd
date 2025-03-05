using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;
using Bootcamp.LaboBackEnd.Domain;
using Konscious.Security.Cryptography;
using System.Text;
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
        using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(utilisateur.PasswordHash)))
        {
            hasher.Iterations = 3;
            hasher.MemorySize = 65536;
            hasher.DegreeOfParallelism = 2;
            utilisateur.PasswordHash = Convert.ToBase64String(hasher.GetBytes(32));
        }

        _utilisateurRepository.Register(utilisateur);
    }

    public Utilisateur? Login(string email, string password)
    {
        string? storedPasswordHash = _utilisateurRepository.GetPasswordHashByEmail(email);

        if (storedPasswordHash is null) return null;

        using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password)))
        {
            hasher.Iterations = 3;
            hasher.MemorySize = 65536;
            hasher.DegreeOfParallelism = 2;

            byte[] computedHash = hasher.GetBytes(32);

            //si true, faire token
            if(AreHashesEqual(Convert.FromBase64String(storedPasswordHash), computedHash))
            {
                return _utilisateurRepository.Login(email);
            }
            return null;
        }
    }
    private bool AreHashesEqual(byte[] storedHash, byte[] computedHash)
    {
        if (storedHash.Length != computedHash.Length) return false;

        for (int i = 0; i < storedHash.Length; i++)
        {
            if (storedHash[i] != computedHash[i]) return false;
        }

        return true;
    }
}