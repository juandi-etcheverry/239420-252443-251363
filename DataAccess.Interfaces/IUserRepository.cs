using System;
using Domain;
namespace DataAccess.Interfaces
{
	public interface IUserRepository
	{
		public User AddUser(User user);
	}
}

