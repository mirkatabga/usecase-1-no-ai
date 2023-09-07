namespace UseCaseOneNoAI.Infrastructure.Exceptions
{
    public class IntegrationException : Exception
    {
        public IntegrationException() { }

        public IntegrationException(string message) : base(message) { }
    }
}
