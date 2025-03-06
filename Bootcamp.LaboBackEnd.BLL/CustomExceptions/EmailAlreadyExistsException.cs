namespace Bootcamp.LaboBackEnd.BLL.CustomExceptions;

public class EmailAlreadyExistsException : Exception
{
    public EmailAlreadyExistsException() : base("Email already exists")
    {
    }

    public EmailAlreadyExistsException(string message) : base(message)
    {
    }
}
