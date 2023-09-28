﻿using System;
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

		public User GetUser(Guid id)
		{
			var user = _userRepository.GetUser(id);
			if(user.IsDeleted) throw new ArgumentException($"User with id {id} not found");
			return user;
        }

        public User CreateUser(User user)
        {
			return _userRepository.AddUser(user);
        }

        public User DeleteUser(Guid id)
        {
			return _userRepository.SoftDelete(id);
        }
    }
}

