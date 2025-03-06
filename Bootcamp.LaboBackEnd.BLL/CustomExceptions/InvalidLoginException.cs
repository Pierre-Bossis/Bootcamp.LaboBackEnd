namespace Bootcamp.LaboBackEnd.BLL.CustomExceptions;

public class InvalidLoginException : Exception
{
    public InvalidLoginException() : base("Invalid email or password") { }
}
