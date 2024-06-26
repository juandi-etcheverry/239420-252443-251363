﻿using DataAccess.Interfaces;
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
        var user = _context.Set<User>().Find(session.User?.Id);
        var cart = _context.Set<Purchase>().Find(session.Cart?.Id);
        session.User = user;
        session.Cart = cart;
        _context.Set<SessionToken>().Add(session);
        _context.SaveChanges();
        return session;
    }

    public SessionToken GetSessionToken(Guid id)
    {
        var session = _context.Set<SessionToken>().Include(s => s.User).FirstOrDefault(x => x.Id == id);
        if (session == null) throw new ArgumentException("Session not found");
        return session;
    }

    public void DeleteSession(Guid id)
    {
        var session = GetSessionToken(id);
        if (session == null) throw new ArgumentException("Session not found");
        _context.Set<SessionToken>().Remove(session);
        _context.SaveChanges();
    }

    public bool SessionTokenExists(Guid id)
    {
        var session = _context.Set<SessionToken>().Find(id);
        if (session == null) return false;
        return true;
    }

    public SessionToken UpdateUserSessionToken(Guid id, User user)
    {
        var sessionToModify = GetSessionToken(id);
        sessionToModify.User = user;
        _context.Set<SessionToken>().Update(sessionToModify);
        _context.SaveChanges();
        return sessionToModify;
    }
}