namespace PassIn.Exceptions;

public class PassInException : SystemException
{
    //Repassar a message para o construtor do SystemException
    public PassInException(string message) : base(message)
    {
        
    }
}
