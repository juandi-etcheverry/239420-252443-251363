using System;
using Domain;
namespace Logic.Interfaces
{
	public interface IUserLogic
	{
		User GetUser(int id);
		User SoftDeleteUser(int id);
		User AddUser(User user);
	}
}

