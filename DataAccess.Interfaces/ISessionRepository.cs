using Domain;

namespace DataAccess.Interfaces;

public interface ISessionRepository
{
    public SessionToken AddSessionToken(SessionToken session);
    public SessionToken GetSessionToken(Guid id);
    public void DeleteSession(Guid id);
    public bool SessionTokenExists(Guid id);
    public SessionToken UpdateUserSessionToken(Guid id, User user);
}