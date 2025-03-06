namespace Bootcamp.LaboBackEnd.BLL.CustomExceptions;

public class UtilisateurNotFoundException : Exception
{
    public UtilisateurNotFoundException() : base("Utilisateur not found.") { }

    public UtilisateurNotFoundException(string message) : base(message) { }
}
