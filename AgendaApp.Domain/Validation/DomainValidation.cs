
namespace AgendaApp.Domain.Validation
{
    internal class DomainValidation : Exception
    {
        public DomainValidation(string error) : base(error) 
        {
        }

        public static void When(bool hasError, string error) 
        {
            if(hasError) 
                throw new Exception(error);
        }
    }
}
