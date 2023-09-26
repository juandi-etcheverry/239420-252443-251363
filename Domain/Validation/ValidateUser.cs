using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validation
{
    internal static class ValidateUser
    {
        internal static void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("User email can't be empty");
        }
    }
}
