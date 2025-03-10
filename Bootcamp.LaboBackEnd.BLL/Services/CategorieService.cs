using Bootcamp.LaboBackEnd.BLL.CustomExceptions;
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

    public Categorie? AddCategorie(string categorie)
    {
        categorie = categorie.Substring(0, 1).ToUpper() + categorie.Substring(1).ToLower();
        bool CategorieNameExists = _Repository.IsCategorieNameExists(categorie);

        if (CategorieNameExists) return null;

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
