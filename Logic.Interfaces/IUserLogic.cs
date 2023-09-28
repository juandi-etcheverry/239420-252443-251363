using System;
using Domain;
namespace Logic.Interfaces
{
	public interface IUserLogic
	{
		User GetUser(Guid id);
		User CreateUser(User user);
        User DeleteUser(Guid id);
    }
}

