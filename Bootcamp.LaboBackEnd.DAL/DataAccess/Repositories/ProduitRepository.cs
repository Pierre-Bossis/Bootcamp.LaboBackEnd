using Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;
using Bootcamp.LaboBackEnd.Domain;
using Microsoft.Data.SqlClient;
using System.Data.Common;

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
        throw new NotImplementedException();
    }

    public Produit DeleteProduit(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Produit> GetAllProduits()
    {
        throw new NotImplementedException();
    }

    public Produit GetProduitById(int id)
    {
        throw new NotImplementedException();
    }

    public Produit UpdateProduit(Produit produit)
    {
        throw new NotImplementedException();
    }
}
