using System;
using Domain;
namespace DataAccess.Interfaces
{
    public interface ISessionRepository
    {
        public SessionToken AddSessionToken(SessionToken session);
    }
}