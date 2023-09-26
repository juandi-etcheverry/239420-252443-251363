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
}