namespace AppEmpleo.Class.Exceptions
{
    public class AppValidationException : AppException
    {
        public AppValidationException(string message) : base(message)
        {
        }

        public AppValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
