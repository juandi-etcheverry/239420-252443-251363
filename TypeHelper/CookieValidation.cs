using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeHelper
{
    public static class CookieValidation
    {
        public static bool AuthExists(string? header)
        {
            if (header is null) return false;
            return Guid.TryParse(header.Split("=")[1], out _);
        }

        public static Guid GetAuthFromHeader(string header)
        {
            return Guid.Parse(header.Split("=")[1]);
        }
    }
}
