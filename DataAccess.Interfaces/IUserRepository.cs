using System;
using Domain;
namespace DataAccess.Interfaces
{
	public interface IUserRepository
	{
		public User AddUser(User user);
		public User GetUser(Guid id);
		public User SoftDelete(Guid id);
	}
}

