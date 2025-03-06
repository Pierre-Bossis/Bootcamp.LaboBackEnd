using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.DAL.DataAccess.Repositories.Interfaces;

public interface ICategorieRepository
{
    IEnumerable<Categorie> GetAllCategories();
    Categorie? GetCategorieById(int id);
    Categorie? GetCategorieByName(string name);
    Categorie AddCategorie(string categorie);
}
