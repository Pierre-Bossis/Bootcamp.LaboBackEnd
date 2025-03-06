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

    public Commande CreateCommande(Commande commande, Guid utilisateurId)
    {
        throw new NotImplementedException();
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
