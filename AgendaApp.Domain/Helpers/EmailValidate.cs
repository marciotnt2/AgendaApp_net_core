using System.Text.RegularExpressions;

namespace AgendaApp.Domain.Validation
{
    public class EmailValidate
    {
        public static bool IsEmail(string? emailToValidate)
        {
            try
            {
                string regex = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";
                return Regex.IsMatch(emailToValidate, regex);
            }
            catch (Exception)
            {

                return false;
            }
        }
           
    }
}
