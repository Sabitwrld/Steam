namespace Steam.Application.Exceptions
{
    /// <summary>
    /// Custom exception to be used when a specific resource is not found.
    /// This will be translated to a 404 Not Found status code in the API.
    /// </summary>
    public class NotFoundException : Exception
    {
        // Constructor to create an exception with a custom message.
        public NotFoundException(string message) : base(message)
        {
        }

        // Constructor to create a standardized message with entity name and key.
        public NotFoundException(string entityName, object key)
            : base($"Entity '{entityName}' with key '{key}' was not found.")
        {
        }
    }
}
