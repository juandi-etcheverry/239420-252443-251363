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
        var sessionExists = _sessionRepository.GetSessionToken(session.Id);
        if (sessionExists != null) return sessionExists;
        var newSession = _sessionRepository.AddSessionToken(session);
        return newSession;
    }
    public bool DeleteSessionToken(SessionToken session)
    {
        _sessionRepository.DeleteSession(session.Id);
        return true;
    }
    
}