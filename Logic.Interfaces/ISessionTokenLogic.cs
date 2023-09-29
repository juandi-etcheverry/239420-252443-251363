using Domain;

namespace Logic.Interfaces;

public interface ISessionTokenLogic
{
    SessionToken GetSessionToken(Guid id);
    SessionToken AddSessionToken(SessionToken session);
    bool DeleteSessionToken(SessionToken session);
    bool SessionTokenExists(Guid id);
    SessionToken AddUserToToken(Guid id, User user); //pre: token exists with no associated user
}