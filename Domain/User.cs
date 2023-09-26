using System;
using System.Reflection.Metadata.Ecma335;
using TypeHelper;

namespace Domain
{
	public class User
	{
		public Guid Id { get; private set; }
		public string Email
        {
            get => Email;
            set
            {
                ValidateEmail(value);
                Email = value;
            }
        }
		public string Password { get; set; }
		public Role Role { get; set; }
		public string Address { get; set; }
        public bool IsDeleted { get; set; } = false;

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("User email can't be empty");
        }
    }
}


