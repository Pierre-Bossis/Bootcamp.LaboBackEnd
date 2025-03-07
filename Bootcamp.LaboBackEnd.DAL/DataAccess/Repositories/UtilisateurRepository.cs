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
        using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            string? storedPasswordHash = null;

            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT PasswordHash FROM Utilisateurs WHERE Email = @Email";
                cmd.Parameters.AddWithValue("@Email", email);

                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    storedPasswordHash = result.ToString();
                }
            }
            return storedPasswordHash;
        }
    }

    public Utilisateur? Login(string email)
    {
        using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, IsAdmin, Email FROM Utilisateurs WHERE Email = @Email";
                cmd.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Utilisateur utilisateur = new Utilisateur
                    {
                        Id = (Guid)reader["Id"],
                        IsAdmin = (bool)reader["IsAdmin"],
                        Email = (string)reader["Email"]
                    };
                    return utilisateur;
                }
                return null;
            }
        }
    }

    public void Register(Utilisateur utilisateur)
    {
        using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();

            using (SqlCommand cmd = connection.CreateCommand())
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

    public IEnumerable<Commande> HistoriqueCommandesByUtilisteurId(Guid utilisateurId)
    {
        using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            Dictionary<int, Commande> commandesDict = new Dictionary<int, Commande>();

            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"
            SELECT 
                c.Id AS CommandeId, 
                c.EtatId, 
                c.UtilisateurId, 
                c.Date, 
                p.Id AS ProduitId, 
                p.Nom AS ProduitNom, 
                p.Prix, 
                cp.Quantite, 
                cat.Id AS CategorieId, 
                cat.Nom AS CategorieNom
            FROM Commandes c
            LEFT JOIN Commandes_Produits cp ON c.Id = cp.CommandeId
            LEFT JOIN Produits p ON cp.ProduitId = p.Id
            LEFT JOIN Categories cat ON p.CategorieId = cat.Id
            WHERE c.UtilisateurId = @utilisateurId";

                cmd.Parameters.AddWithValue("@utilisateurId", utilisateurId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int commandeId = reader.GetInt32(reader.GetOrdinal("CommandeId"));

                        if (!commandesDict.TryGetValue(commandeId, out Commande commande))
                        {
                            commande = new Commande
                            {
                                Id = commandeId,
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                EtatId = reader.GetInt32(reader.GetOrdinal("EtatId")),
                                UtilisateurId = utilisateurId,
                                Produits = new List<Produit>() // Initialise la liste des produits
                            };
                            commandesDict[commandeId] = commande;
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("ProduitId")))
                        {
                            int produitId = reader.GetInt32(reader.GetOrdinal("ProduitId"));

                            if (!commande.Produits.Any(p => p.Id == produitId))
                            {
                                Produit produit = new Produit
                                {
                                    Id = produitId,
                                    Nom = reader.GetString(reader.GetOrdinal("ProduitNom")),
                                    Prix = reader.GetDecimal(reader.GetOrdinal("Prix")),
                                    Quantite = reader.GetInt32(reader.GetOrdinal("Quantite")),
                                    Categorie = new Categorie
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("CategorieId")),
                                        Nom = reader.GetString(reader.GetOrdinal("CategorieNom"))
                                    }
                                };
                                commande.Produits.Add(produit);
                            }
                        }
                    }
                }
            }
            return commandesDict.Values;
        }
    }

}
