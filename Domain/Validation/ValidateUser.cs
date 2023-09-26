using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Validation
{
    internal static class ValidateUser
    {
        internal static void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("User email can't be empty");
            if (!IsValidEmail(email)) throw new ArgumentException("User email format is incorrect");
        }

        internal static void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("User password can't be empty");
            if(password.Length < 5) throw new ArgumentException("User password must be at least 5 characters");
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();

                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
