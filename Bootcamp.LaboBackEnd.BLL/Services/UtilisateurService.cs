﻿using Bootcamp.LaboBackEnd.BLL.CustomExceptions;
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

    public bool Register(Utilisateur utilisateur)
    {
        bool emailExists = _utilisateurRepository.IsEmailAlreadyExists(utilisateur.Email);
        //if (emailExists) throw new EmailAlreadyExistsException();
        if (emailExists) return false;

        utilisateur.PasswordHash = Argon2.Hash(utilisateur.PasswordHash);

        return _utilisateurRepository.Register(utilisateur);
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
        return _utilisateurRepository.HistoriqueCommandesByUtilisteurId(utilisateurId);
    }
}