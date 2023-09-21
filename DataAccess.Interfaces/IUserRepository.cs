using System;
using Domain;
namespace DataAccess.Interfaces
{
	public interface IUserRepository
	{
		public User AddUser(User user);
		public User GetUser(int id);
		public User SoftDelete(int id);
	}
}

