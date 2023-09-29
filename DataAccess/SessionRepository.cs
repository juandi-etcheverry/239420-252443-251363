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
    
    public SessionToken GetSessionToken(Guid id)
    {
        var session = _context.Set<SessionToken>().Find(id);
        if (session == null) throw new ArgumentException($"Session not found");
        return session;
    }

    public SessionToken GetSessionToken(User user)
    {
        var session = _context.Set<SessionToken>().FirstOrDefault(s => s.User == user);
        if (session == null) throw new ArgumentException($"User has no active session");
        return session;
    }

    public void DeleteSession(Guid id)
    {
        var session = GetSessionToken(id);
        if (session == null) throw new ArgumentException($"Session not found");
        _context.Set<SessionToken>().Remove(session);
        _context.SaveChanges();
    }
}