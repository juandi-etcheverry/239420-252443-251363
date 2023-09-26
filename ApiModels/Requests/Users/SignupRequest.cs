using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeHelper;

namespace ApiModels.Requests.Users
{
    public class SignupRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
