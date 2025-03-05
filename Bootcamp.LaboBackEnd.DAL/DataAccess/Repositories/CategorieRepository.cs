using Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;
using Bootcamp.LaboBackEnd.Domain;
using Microsoft.Data.SqlClient;

namespace Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories;

public class CategorieRepository : ICategorieRepository
{
    private readonly SqlConnection _connection;

    public CategorieRepository(SqlConnection connection)
    {
        _connection = connection;
    }

    public Categorie AddCategorie(string nom)
    {
        try
        {
            _connection.Open();

            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Categories (Nom) OUTPUT INSERTED.Id VALUES (@Nom)";
                cmd.Parameters.AddWithValue("@Nom", nom);

                int insertedId = (int)cmd.ExecuteScalar();
                _connection.Close();

                return new Categorie()
                {
                    Id = insertedId,
                    Nom = nom
                };
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Erreur lors de l'ajout de la catégorie", ex);
        }
    }
    public IEnumerable<Categorie> GetAllCategories()
    {
        _connection.Open();

        using (SqlCommand cmd = _connection.CreateCommand())
        {
            cmd.CommandText = "SELECT * FROM Categories";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new Categorie()
                {
                    Id = (int)reader["Id"],
                    Nom = (string)reader["Nom"]
                };
            }
            _connection.Close();
        }
    }
    public Categorie? GetCategorieById(int id)
    {
        _connection.Open();

        using (SqlCommand cmd = _connection.CreateCommand())
        {
            cmd.CommandText = "SELECT * FROM Categories WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Categorie categorie = new Categorie()
                    {
                        Id = (int)reader["Id"],
                        Nom = (string)reader["Nom"]
                    };
                    _connection.Close();
                    return categorie;
                }
                _connection.Close();
                return null;
            };
        }
    }

    public Categorie GetCategorieByName(string name)
    {
        _connection.Open();

        using (SqlCommand cmd = _connection.CreateCommand())
        {
            cmd.CommandText = "SELECT * FROM Categories WHERE Nom = @nom";
            cmd.Parameters.AddWithValue("@nom", name);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Categorie categorie = new()
                    {
                        Id = (int)reader["Id"],
                        Nom = (string)reader["Nom"]
                    };
                    _connection.Close();
                    return categorie;
                }
                _connection.Close();
                return null;
            };
        }
    }
}
