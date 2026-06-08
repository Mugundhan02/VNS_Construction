namespace BuildManager.Exceptions
{
    public class UnableToCreateEntityException : Exception
    {
        public UnableToCreateEntityException(string entity, string reason)
            : base($"Unable to create {entity}: {reason}") { }
    }
}
