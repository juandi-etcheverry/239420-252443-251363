using System;
using DataAccess.Interfaces;
using Domain;
using Logic.Interfaces;
namespace Logic
{
	public class UserLogic : IUserLogic
	{
		private IUserRepository _userRepository;

		public UserLogic(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public User GetUser(Guid id)
		{
			var user = _userRepository.GetUser(id);
			if(user.IsDeleted) throw new ArgumentException($"User with id {id} not found");
			return user;
        }

        public User GetUser(string email, string password)
        {
            var user = _userRepository.GetUser(email, password);
            if (user.IsDeleted) throw new UnauthorizedAccessException($"User with email {email} not found");
            return user;
        }

        public User CreateUser(User user)
        {
			bool exists = _userRepository.FindUser(user.Email);
            if (exists) throw new ArgumentException($"User with email {user.Email} already exists");
            return _userRepository.AddUser(user);
        }

        public User DeleteUser(Guid id)
        {
			return _userRepository.SoftDelete(id);
        }

        public User UpdateUser(Guid id, User user)
        {
			return _userRepository.UpdateUser(id, user);
        }
    }
}

