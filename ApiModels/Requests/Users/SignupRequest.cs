using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using TypeHelper;

namespace ApiModels.Requests.Users
{
    public class SignupRequest
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        private string _passwordConfirmation { get; set; }
        public string PasswordConfirmation
        {
            get => _passwordConfirmation;
            set
            {
                ValidatePasswordMatch(value);
                _passwordConfirmation = value;
            }

        }

        private void ValidatePasswordMatch(string confirmation)
        {
            if(confirmation != Password) throw new ArgumentException("Passwords do not match");
        }


        public User ToEntity()
        {
           return new User()
            {
                Email = Email,
                Address = Address,
                Role = Role.Comprador,
                Password = Password
            };
        }
    }
}
