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

}