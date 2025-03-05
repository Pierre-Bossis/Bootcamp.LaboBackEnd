using Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;
using Bootcamp.LaboBackEnd.Domain;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories;

public class UtilisateurRepository : IUtilisateurRepository
{
    private readonly SqlConnection _connection;

    public UtilisateurRepository(SqlConnection connection)
    {
        _connection = connection;
    }

    public string? GetPasswordHashByEmail(string email)
    {
        string? storedPasswordHash = null;

        using (SqlCommand cmd = _connection.CreateCommand())
        {
            cmd.CommandText = "SELECT PasswordHash FROM Utilisateurs WHERE Email = @Email";
            cmd.Parameters.AddWithValue("@Email", email);

            _connection.Open();
            var result = cmd.ExecuteScalar();
            _connection.Close();

            if (result != null)
            {
                storedPasswordHash = result.ToString();
            }
        }
        return storedPasswordHash;
    }

    public Utilisateur? Login(string email)
    {
        using (SqlCommand cmd = _connection.CreateCommand())
        {
            cmd.CommandText = "SELECT Id, IsAdmin, Email FROM Utilisateurs WHERE Email = @Email";
            cmd.Parameters.AddWithValue("@Email", email);

            _connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Utilisateur utilisateur = new Utilisateur
                {
                    Id = (Guid)reader["Id"],
                    IsAdmin = (bool)reader["IsAdmin"],
                    Email = (string)reader["Email"]
                };
                _connection.Close();
                return utilisateur;
            }
            return null;
        }
    }

    public void Register(Utilisateur utilisateur)
    {
        _connection.Open();

        using (SqlCommand cmd = _connection.CreateCommand())
        {
            cmd.CommandText = "INSERT INTO Utilisateurs (Nom, Prenom, Email, PasswordHash) VALUES (@nom, @prenom, @email, @passwordHash);";
            cmd.Parameters.AddWithValue("@nom", utilisateur.Nom);
            cmd.Parameters.AddWithValue("@prenom", utilisateur.Prenom);
            cmd.Parameters.AddWithValue("@email", utilisateur.Email);
            cmd.Parameters.AddWithValue("@passwordHash", utilisateur.PasswordHash);

            cmd.ExecuteNonQuery();
        }
    }


}
