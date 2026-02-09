namespace AppEmpleo.Class.Exceptions
{
    public class AppAuthorizationException : AppException
    {
        public AppAuthorizationException(string message) : base(message)
        {
        }

        public AppAuthorizationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
