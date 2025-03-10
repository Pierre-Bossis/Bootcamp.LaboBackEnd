namespace Bootcamp.LaboBackEnd.BLL.CustomExceptions;

public class NameCategorieAlreadyExistsException : Exception
{
    public NameCategorieAlreadyExistsException() : base("Le nom de la catégorie existe déjà.") { }

    public NameCategorieAlreadyExistsException(string message) : base(message) { }
}
