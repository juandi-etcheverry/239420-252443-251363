using System;
using System.Reflection.Metadata.Ecma335;
using Domain.Validation;
using TypeHelper;

namespace Domain
{
	public class User
	{
		public Guid Id { get; set; }
        private string _email;
		public string Email
        {
            get => _email;
            set
            {
                ValidateUser.ValidateEmail(value);
                _email = value;
            }
        }
        private string _password;
		public string Password
        {
            get => _password;
            set
            {
                ValidateUser.ValidatePassword(value);
                _password = value;
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                ValidateUser.ValidateAddress(value);
                _address = value;
            }
        }
        public Role Role { get; set; }
		
        public bool IsDeleted { get; set; } = false;
        public SessionToken? Session { get; set; }
    }
}


