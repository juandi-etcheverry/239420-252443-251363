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
		public User GetUser(int id)
		{
			var user = _context.Set<User>().Find(id);
			if(user == null) throw new ArgumentException($"User with id {id} not found");
			return user;
        }
		public User SoftDelete(int id)
		{
			var user = _context.Set<User>().Find(id);
			if(user == null) throw new ArgumentException($"User with id {id} not found");
            user.IsDeleted = true;
			_context.SaveChanges();
			return user;
        }
	}
}

