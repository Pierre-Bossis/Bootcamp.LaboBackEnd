﻿using Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;
using Bootcamp.LaboBackEnd.Domain;
using Microsoft.Data.SqlClient;

namespace Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories;

public class CommandeRepository : ICommandeRepository
{
    private readonly SqlConnection _connection;

    public CommandeRepository(SqlConnection connection)
    {
        _connection = connection;
    }

    public bool CreateCommande(Guid utilisateurId, IEnumerable<Commande_Produit> produits)
    {
        using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();

            using (SqlCommand cmd = connection.CreateCommand())
            {
                SqlTransaction transaction = connection.BeginTransaction();
                cmd.Transaction = transaction;

                try
                {
                    cmd.CommandText = "INSERT INTO Commandes (UtilisateurId) VALUES (@utilisateurId); SELECT SCOPE_IDENTITY()";
                    cmd.Parameters.AddWithValue("@utilisateurId", utilisateurId);

                    int commandeId = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (Commande_Produit produit in produits)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "INSERT INTO Commandes_Produits (CommandeId, ProduitId, Quantite) VALUES (@commandeId, @produitId, @quantite)";
                        cmd.Parameters.AddWithValue("@commandeId", commandeId);
                        cmd.Parameters.AddWithValue("@produitId", produit.ProduitId);
                        cmd.Parameters.AddWithValue("@quantite", produit.Quantite);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                    //throw;
                    //des logs ici pour tracer l'exception ?
                }
            }
            return true;
        }
    }

    public IEnumerable<Commande> GetAllCommandes()
    {
        using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            Dictionary<int, Commande> commandeDict = new Dictionary<int, Commande>();

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
                LEFT JOIN Categories cat ON p.CategorieId = cat.Id";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int commandeId = reader.GetInt32(reader.GetOrdinal("CommandeId"));

                        // Vérifier si la commande existe déjà
                        if (!commandeDict.TryGetValue(commandeId, out Commande commande))
                        {
                            commande = new Commande()
                            {
                                Id = commandeId,
                                EtatId = reader.GetInt32(reader.GetOrdinal("EtatId")),
                                UtilisateurId = reader.GetGuid(reader.GetOrdinal("UtilisateurId")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Produits = new List<Produit>()
                            };
                            commandeDict[commandeId] = commande;
                        }

                        // Vérifier si un produit est attaché
                        if (!reader.IsDBNull(reader.GetOrdinal("ProduitId")))
                        {
                            Produit produit = new Produit()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ProduitId")),
                                Nom = reader.GetString(reader.GetOrdinal("ProduitNom")),
                                Prix = reader.GetDecimal(reader.GetOrdinal("Prix")),
                                Quantite = reader.GetInt32(reader.GetOrdinal("Quantite")),
                                Categorie = new Categorie()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("CategorieId")),
                                    Nom = reader.GetString(reader.GetOrdinal("CategorieNom"))
                                }
                            };

                            commande.Produits.Add(produit);
                        }
                    }

                    return commandeDict.Values;
                }
            }
        }
    }

    public Commande? GetCommandeById(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();

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
                p.Description AS ProduitDescription,
                p.Prix, 
                cp.Quantite, 
                cat.Id AS CategorieId, 
                cat.Nom AS CategorieNom
            FROM Commandes c
            LEFT JOIN Commandes_Produits cp ON c.Id = cp.CommandeId
            LEFT JOIN Produits p ON cp.ProduitId = p.Id
            LEFT JOIN Categories cat ON p.CategorieId = cat.Id
            WHERE c.Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Commande? commande = null;

                    while (reader.Read())
                    {
                        if (commande == null)
                        {
                            commande = new Commande
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CommandeId")),
                                EtatId = reader.GetInt32(reader.GetOrdinal("EtatId")),
                                UtilisateurId = reader.GetGuid(reader.GetOrdinal("UtilisateurId")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Produits = new List<Produit>()
                            };
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("ProduitId")))
                        {
                            Produit produit = new Produit
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ProduitId")),
                                Nom = reader.GetString(reader.GetOrdinal("ProduitNom")),
                                Prix = reader.GetDecimal(reader.GetOrdinal("Prix")),
                                Description = reader.GetString(reader.GetOrdinal("ProduitDescription")),
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
                    return commande;
                }
            }
        }
    }

    public Commande UpdateStateCommande(int id, int stateId)
    {
        using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();

            using (SqlCommand cmd = connection.CreateCommand())
            {
                // Mise à jour de l'état de la commande
                cmd.CommandText = "UPDATE Commandes SET EtatId = @stateId WHERE Id = @id";
                cmd.Parameters.AddWithValue("@stateId", stateId);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, EtatId, UtilisateurId, Date FROM Commandes WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Commande
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            EtatId = reader.GetInt32(reader.GetOrdinal("EtatId")),
                            UtilisateurId = reader.GetGuid(reader.GetOrdinal("UtilisateurId")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date"))
                        };
                    }
                }
            }
        }
        throw new Exception("Problème lors de la mise à jour.");
    }

}
