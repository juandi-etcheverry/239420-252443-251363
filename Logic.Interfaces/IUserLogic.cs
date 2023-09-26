using System;
using Domain;
namespace Logic.Interfaces
{
	public interface IUserLogic
	{
		User GetUser(int id);
		User CreateUser(User user);
	}
}

