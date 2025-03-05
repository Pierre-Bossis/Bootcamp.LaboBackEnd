using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;

public interface IUtilisateurRepository
{
    void Register(Utilisateur utilisateur);
    Utilisateur Login(string email);
    string GetPasswordHashByEmail(string email);
}
