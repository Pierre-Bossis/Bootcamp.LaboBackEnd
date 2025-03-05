using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;
using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.BLL.Services;

public class CategorieService : ICategorieService
{
    private readonly ICategorieRepository _Repository;

    public CategorieService(ICategorieRepository repository)
    {
        _Repository = repository;
    }

    public Categorie AddCategorie(string categorie)
    {
        return _Repository.AddCategorie(categorie);
    }

    public IEnumerable<Categorie> GetAllCategories()
    {
        return _Repository.GetAllCategories();
    }

    public Categorie GetCategorieById(int id)
    {
        return _Repository.GetCategorieById(id);
    }

    public Categorie GetCategorieByName(string name)
    {
        return _Repository.GetCategorieByName(name);
    }
}
