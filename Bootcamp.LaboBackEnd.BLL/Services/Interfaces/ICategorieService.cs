using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.BLL.Services.Interfaces;

public  interface ICategorieService
{
    IEnumerable<Categorie> GetAllCategories();
    Categorie GetCategorieById(int id);
    Categorie GetCategorieByName(string name);
    Categorie AddCategorie(string categorie);
}
