using System;
using Domain;
namespace DataAccess.Interfaces
{
    public interface ISessionRepository
    {
        public SessionToken AddSessionToken(SessionToken session);
        public SessionToken GetSessionToken(Guid id);
        public void DeleteSession(Guid id);
    }
}