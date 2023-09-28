using System;
using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class SessionRepository : ISessionRepository
{
    private readonly DbContext _context;

    public SessionRepository(DbContext context)
    {
        _context = context;
    }
    public SessionToken AddSessionToken(SessionToken session)
    {
        _context.Set<SessionToken>().Add(session);
        _context.SaveChanges();
        return session;
    }
    public List<SessionToken> GetUserSessions(User user)
    {
        var result =_context.Set<SessionToken>().Where(s => s.User.Id == user.Id);
        return result.ToList();
    }
    
    public SessionToken GetSessionToken(Guid id)
    {
        var session = _context.Set<SessionToken>().Find(id);
        if (session == null) throw new ArgumentException($"Session not found");
        return session;
    }
}