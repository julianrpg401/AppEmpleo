namespace AppEmpleo.Class.Exceptions
{
    public class AppAuthenticationException : AppException
    {
        public AppAuthenticationException(string message) : base(message)
        {
        }

        public AppAuthenticationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
