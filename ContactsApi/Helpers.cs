namespace ContactsApi.Helpers
{
    public static class ErrorHelper
    {
        public const string EntityNotFoundMessage = "Entity not found";
        public const string BadRequestMessage = "Bad request";

        // Puedes agregar más constantes según sea necesario.

        public static void ThrowEntityNotFoundException()
        {
            throw new EntityNotFoundException(EntityNotFoundMessage);
        }

        public static void ThrowBadRequestException()
        {
            throw new BadRequestException(BadRequestMessage);
        }


    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }
}
}

