using Bootcamp.LaboBackEnd.Domain;

namespace Bootcamp.LaboBackEnd.BLL.Services.Interfaces;

public  interface ICategorieService
{
    /// <summary>
    /// Récupérer toutes les catégories.
    /// </summary>
    /// <returns>Une IEnumerable de <see cref="Categorie"/></returns>
    IEnumerable<Categorie> GetAllCategories();
    /// <summary>
    /// Récupérer une catégorie par son Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Une instance de <see cref="Categorie"/></returns>
    Categorie GetCategorieById(int id);
    /// <summary>
    /// Récupérer une catégorie par son Nom.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Une instance de <see cref="Categorie"/></returns>
    Categorie GetCategorieByName(string name);
    /// <summary>
    /// Créer une catégorie.
    /// </summary>
    /// <param name="categorie"></param>
    /// <returns>L'instance de la <see cref="Categorie"/> construite.</returns>
    Categorie AddCategorie(string categorie);
}
