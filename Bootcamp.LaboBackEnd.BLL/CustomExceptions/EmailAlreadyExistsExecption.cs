namespace Bootcamp.LaboBackEnd.BLL.CustomExceptions;

public class EmailAlreadyExistsExecption : Exception
{
    public EmailAlreadyExistsExecption() : base("Email already exists")
    {
    }

    public EmailAlreadyExistsExecption(string message) : base(message)
    {
    }
}
