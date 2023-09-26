using System;
using Domain;
namespace DataAccess.Interfaces
{
    public interface ISessionRepository
    {
        public SessionToken AddSessionToken(SessionToken session);
        public List<SessionToken> GetUserSessions(User user);
        public SessionToken GetSessionToken(Guid id);
    }
}