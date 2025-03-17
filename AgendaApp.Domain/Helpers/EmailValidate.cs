using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
