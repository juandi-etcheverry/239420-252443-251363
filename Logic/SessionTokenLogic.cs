using DataAccess.Interfaces;
using Domain;
using Logic.Interfaces;
namespace Logic;

public class SessionTokenLogic : ISessionTokenLogic
{
    private ISessionRepository _sessionRepository;
    
    public SessionTokenLogic(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }
    public SessionToken GetSessionToken(Guid id)
    {
        var session = _sessionRepository.GetSessionToken(id);
        return session;
    }
    public SessionToken AddSessionToken(SessionToken session)
    {
        try
        {
            var sessionExists = _sessionRepository.GetSessionToken(session.Id);
            return sessionExists;
        }
        catch (ArgumentException e)
        {
            var newSession = _sessionRepository.AddSessionToken(session);
            return newSession;
        }

    }
    public bool DeleteSessionToken(SessionToken session)
    {
        _sessionRepository.DeleteSession(session.Id);
        return true;
    }

    public bool SessionTokenExists(Guid id)
    {
        return _sessionRepository.SessionTokenExists(id);
    }

    public SessionToken AddUserToToken(Guid id, User user)
    {
        return _sessionRepository.UpdateUserSessionToken(id, user);
    }
    public void UpdateCart(Guid id, Purchase purchase)
    {
        var session = _sessionRepository.GetSessionToken(id);
        session.Cart = purchase;
        _sessionRepository.UpdateUserSessionToken(id, session.User);
    }
}