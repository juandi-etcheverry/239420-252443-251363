using Domain;

namespace Logic.Interfaces;

public interface ISessionTokenLogic
{
    SessionToken GetSessionToken(Guid id);
    SessionToken AddSessionToken(SessionToken session);
    bool DeleteSessionToken(SessionToken session);
}