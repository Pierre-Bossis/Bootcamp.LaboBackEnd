namespace Bootcamp.LaboBackEnd.BLL.CustomExceptions;

public class NotFoundCommandeException : Exception
{
    public NotFoundCommandeException() : base("La commande n'a pas été trouvée.") { }

    public NotFoundCommandeException(string message, Exception innerException) : base(message, innerException) { }
}
