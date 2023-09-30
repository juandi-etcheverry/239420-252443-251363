using System;
using Domain;
namespace Logic.Interfaces
{
	public interface IUserLogic
	{
		User GetUser(Guid id);
        User GetUser(string email, string password);
		User CreateUser(User user);
        User DeleteUser(Guid id);
        User UpdateUser(Guid id, User user);
    }
}

