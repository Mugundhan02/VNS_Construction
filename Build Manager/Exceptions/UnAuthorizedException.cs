namespace BuildManager.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException(string message = "You are not authorized to perform this action.")
            : base(message) { }
    }
}
