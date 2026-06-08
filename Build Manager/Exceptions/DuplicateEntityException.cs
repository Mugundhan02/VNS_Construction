namespace BuildManager.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException(string entity, string field, string value)
            : base($"{entity} with {field} '{value}' already exists.") { }
    }
}
