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
		public User GetUser(int id)
		{
			var user = _userRepository.GetUser(id);
			if(user.IsDeleted) throw new ArgumentException($"User with id {id} not found");
			return user;
        }
		public User SoftDeleteUser(int id)
		{
			var user = _userRepository.SoftDeleteUser(id);
			return user;
		}
		public User AddUser(User user)
		{
			var result = _userRepository.AddUser(user);
			return result;
		}
	}
}

