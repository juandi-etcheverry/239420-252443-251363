using System;
using Domain;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
	public class UserRepository : IUserRepository
	{
        private readonly DbContext _context;
		
        public UserRepository(DbContext context)
		{
			_context = context;
		}

		public User AddUser(User user)
		{
			_context.Set<User>().Add(user);
			_context.SaveChanges();
			return user;
		}
	}
}

