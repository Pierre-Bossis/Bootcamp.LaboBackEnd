using Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;
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
        throw new NotImplementedException();
    }

    public Commande UpdateStateCommande(int id, int stateId)
    {
        throw new NotImplementedException();
    }
}
