using Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;
using Bootcamp.LaboBackEnd.Domain;
using Microsoft.Data.SqlClient;

namespace Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories;

public class ProduitRepository : IProduitRepository
{
    private readonly SqlConnection _connection;

    public ProduitRepository(SqlConnection connection)
    {
        _connection = connection;
    }

    public Produit AddProduit(Produit produit)
    {
        using (SqlConnection connection = new(_connection.ConnectionString))
        {
            connection.Open();

            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Produits (Nom, Description, Prix, Quantite, CategorieId) VALUES (@Nom, @Description, @Prix, @Quantite, @CategorieId); SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("@Nom", produit.Nom);
                cmd.Parameters.AddWithValue("@Description", produit.Description);
                cmd.Parameters.AddWithValue("@Prix", produit.Prix);
                cmd.Parameters.AddWithValue("@Quantite", produit.Quantite);
                cmd.Parameters.AddWithValue("@CategorieId", produit.CategorieId);

                object result = cmd.ExecuteScalar();
                int insertedId = Convert.ToInt32((decimal)result);

                if (insertedId > 0)
                {
                    produit.Id = insertedId;
                    return produit;
                }
                throw new Exception("Problème lors de la création");
            }
        }
    }

    public bool DeleteProduit(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();

            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Produits WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected == 1;
            }
        }
    }

    public IEnumerable<Produit> GetAllProduits()
    {
        using (SqlConnection connection = new(_connection.ConnectionString))
        {
            connection.Open();
            ICollection<Produit> produits = new List<Produit>();

            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, Nom, Description, Prix, Quantite, CategorieId FROM Produits";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produits.Add(new Produit
                        {
                            Id = reader.GetInt32(0),
                            Nom = reader.GetString(1),
                            Description = reader.GetString(2),
                            Prix = reader.GetDecimal(3),
                            Quantite = reader.GetInt32(4),
                            CategorieId = reader.GetInt32(5)
                        });
                    }
                }
            }
            return produits;
        }
    }

    public Produit? GetProduitById(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();

            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"
                SELECT 
                    p.Id, p.Nom, p.Description, p.Prix, p.Quantite, p.CategorieId,
                    c.Id AS CategorieId, c.Nom AS CategorieNom
                FROM Produits p
                INNER JOIN Categories c ON p.CategorieId = c.Id
                WHERE p.Id = @Id";

                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Produit
                        {
                            Id = reader.GetInt32(0),
                            Nom = reader.GetString(1),
                            Description = reader.GetString(2),
                            Prix = reader.GetDecimal(3),
                            Quantite = reader.GetInt32(4),
                            CategorieId = reader.GetInt32(5),
                            Categorie = new Categorie
                            {
                                Id = reader.GetInt32(6),
                                Nom = reader.GetString(7)
                            }
                        };
                    }
                    return null;
                }
            }
        }
    }

    public Produit UpdateProduit(int id, Produit produit)
    {
        using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();

            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE Produits SET Nom = @Nom, Description = @Description, Prix = @Prix, Quantite = @Quantite, CategorieId = @CategorieId WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Nom", produit.Nom);
                cmd.Parameters.AddWithValue("@Description", produit.Description);
                cmd.Parameters.AddWithValue("@Prix", produit.Prix);
                cmd.Parameters.AddWithValue("@Quantite", produit.Quantite);
                cmd.Parameters.AddWithValue("@CategorieId", produit.CategorieId);
                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 1) return produit;

                throw new Exception("Problème lors de la mise à jour");
            }
        }
    }
}
